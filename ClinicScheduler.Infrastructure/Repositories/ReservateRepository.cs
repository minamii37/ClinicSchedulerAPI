using System;
using System.Net.Http;
using ClinicScheduler.Domain.Models.ReservationDomainModel;
using ClinicScheduler.Domain.IRepositories;
using ClinicScheduler.Infrastructure.Models;
using Newtonsoft.Json;
using System.Net;
using ClinicScheduler.Domain.Models.ReservationDomainModel.ValueObjects;
using ClinicScheduler.Infrastructure.DBAccess;

namespace ClinicScheduler.Infrastructure.Repositories
{
    public class ReservateRepository : IReservateRepository
    {
        private readonly string directoryPath = "../ClinicScheduler.Infrastructure/Data";

        public ReservateRepository()
        {
        }

        public IEnumerable<ReservationDomainModel> GetOwnReservations(string patientId)
        {
            var reservations = GetAllReservationsFromDB().Where(
                x => x.PatientId.Equals(patientId) && x.TargetDateTime >= DateTime.Now);
            if (!reservations.Any())
            {
                // 現在予約中の情報がなければ空で返却
                return Enumerable.Empty<ReservationDomainModel>();
            }

            // 予約に紐づくドクターIDとその情報の取得
            var doctorIds = reservations.Select(x => x.DoctorId).Distinct();
            var doctorInfomations = GetDoctorInfomations(doctorIds);

            // ドメインモデル変換と返却
            var models = new List<ReservationDomainModel>();
            foreach (var reservation in reservations)
            {
                var model = ConvertReservationModel(reservation);
                models.Add(model);
            }

            return models;
        }

        public IEnumerable<ReservationDomainModel> GetRelatedExistingReservation(ReservationDomainModel request)
        {
            // 関連既存予約の取得
            var repositoryModels = GetAllReservationsFromDB();

            // 対象の日時か、患者IDとドクターIDで未完了の予約を絞り込み
            repositoryModels = repositoryModels?.Where(
                x => x.TargetDateTime == request.TargetDateTime
                    || (request.PatientId!.Equals(x.PatientId)
                    && request.DoctorId.Equals(x.DoctorId)
                    && x.TargetDateTime < DateTime.Now))
                ?? Enumerable.Empty<ReservationRepositoryModel>();

            var domainmodels = new List<ReservationDomainModel>();
            foreach (var repositoryModel in repositoryModels)
            {
                domainmodels.Add(ConvertReservationModel(repositoryModel));
            }

            return domainmodels;
        }

        public ReservationDomainModel GetSpecifiedDateReservation(DateTime specifiedDateTime)
        {
            var reservation = GetAllReservationsFromDB().FirstOrDefault(x => x.TargetDateTime == specifiedDateTime);
            if (reservation is null)
            {
                return new ReservationDomainModel(string.Empty, string.Empty, specifiedDateTime, string.Empty, null);
            }
            return ConvertReservationModel(reservation);
        }

        public ReservationDomainModel PostReservation(ReservationDomainModel request)
        {
            // JSONファイル書き込みのため、全データ取得
            var reservations = GetAllReservationsFromDB();

            // 追加予約データの作成
            var requestModel = new ReservationRepositoryModel
            {
                ReservationId = Guid.NewGuid().ToString(),
                DoctorId = request.DoctorId,
                TargetDateTime = request.TargetDateTime,
                PatientId = request.PatientId!,
                ReservationDateTime = DateTime.Now
            };

            // 取得データに予約データを追加し、書き込みの実施
            reservations = reservations?.Append(requestModel);
            string json = JsonConvert.SerializeObject(reservations, Formatting.Indented);
            File.WriteAllText(@$"{ directoryPath}/ReservationTable.json", json);

            return new ReservationDomainModel(
                requestModel.ReservationId,
                request.DoctorId,
                request.TargetDateTime,
                request.PatientId,
                requestModel.ReservationDateTime);
        }

        public IEnumerable<DoctorInfoModel> GetTargetDoctorList(IEnumerable<string> doctorIds)
        {
            var repositoryModels = new MstDoctorInfomations()
                .GetAllDoctorInfomationsFromDB().Where(x => doctorIds.Contains(x.DoctorId));

            var valueObjects = new List<DoctorInfoModel>();
            foreach (var repositoryModel in repositoryModels)
            {
                var domainModel = ConvertDoctorInfoModel(repositoryModel);
                valueObjects.Add(domainModel);
            }
            return valueObjects;
        }

        public IEnumerable<PatientInfoModel> GetTargetPatientList(IEnumerable<string> patientIds)
        {
            var repositoryModels = new MstPatientInfomations()
                .GetAllPatientInfoFromDB().Where(x => patientIds.Contains(x.PatientId));

            var valueObjects = new List<PatientInfoModel>();
            foreach (var repositoryModel in repositoryModels)
            {
                var valueObject = ConvertPatientInfoModel(repositoryModel);
                valueObjects.Add(valueObject);
            }
            return valueObjects;
        }

        /// <summary>
        /// 予約テーブルデータの全取得
        /// </summary>
        /// <returns></returns>
        private IEnumerable<ReservationRepositoryModel> GetAllReservationsFromDB()
        {
            // JSONデータの取得
            StreamReader r = new StreamReader($"{directoryPath}/ReservationTable.json");
            string jsonString = r.ReadToEnd();
            // JSONデータのデシリアライズ
            IEnumerable<ReservationRepositoryModel>? repositoryModels =
                JsonConvert.DeserializeObject<IEnumerable<ReservationRepositoryModel>>(jsonString);

            return repositoryModels ?? Enumerable.Empty<ReservationRepositoryModel>();
        }

        /// <summary>
        /// ドクター情報の取得
        /// </summary>
        /// <returns></returns>
        private IEnumerable<DoctorInfoRepositoryModel> GetDoctorInfomations(IEnumerable<string> doctorIdList)
        {
            // JSONデータの取得
            StreamReader r = new StreamReader($"{directoryPath}/DoctorInfoTable.json");
            string jsonString = r.ReadToEnd();
            // JSONデータのデシリアライズ
            IEnumerable<DoctorInfoRepositoryModel>? repositoryModels =
                JsonConvert.DeserializeObject<IEnumerable<DoctorInfoRepositoryModel>>(jsonString);

            // ドクター情報で絞り込み
            repositoryModels = repositoryModels?.Where(x => doctorIdList.Contains(x.DoctorId));

            if (repositoryModels?.Count() == 0)
            {
                throw new HttpRequestException("対象の医師IDは存在しません", null, HttpStatusCode.NotFound);
            }

            return repositoryModels ?? Enumerable.Empty<DoctorInfoRepositoryModel>();
        }

        /// <summary>
        /// 予約情報リポジトリモデル→ドメインモデルの変換
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns></returns>
        private static ReservationDomainModel ConvertReservationModel(ReservationRepositoryModel reservation)
            => new ReservationDomainModel(
                reservation.ReservationId,
                reservation.DoctorId,
                reservation.TargetDateTime,
                reservation.PatientId,
                reservation.ReservationDateTime);

        /// <summary>
        /// 医師情報リポジトリモデル→ValueObjectモデル
        /// </summary>
        /// <param name="doctorInfo"></param>
        /// <returns></returns>
        private static DoctorInfoModel ConvertDoctorInfoModel(DoctorInfoRepositoryModel doctorInfo)
            => new DoctorInfoModel(doctorInfo.DoctorId, doctorInfo.DoctorName, doctorInfo.CreateDate);

        /// <summary>
        /// 患者情報リポジトリモデル→ValueObjectモデル
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        private static PatientInfoModel ConvertPatientInfoModel(PatientInfoRepositoryModel patientInfo)
            => new PatientInfoModel(patientInfo.PatientId, patientInfo.PatientName, patientInfo.CreateDate);
    }
}

