using System;
using System.Net.Http;
using ClinicScheduler.Domain.Models.ReservationDomainModel;
using ClinicScheduler.Domain.IRepositories;
using ClinicScheduler.Infrastructure.Models;
using Newtonsoft.Json;
using System.Net;

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
                var model = ConvertModel(reservation, doctorInfomations.First(x => x.DoctorId == reservation.DoctorId));
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
                domainmodels.Add(ConvertModel(repositoryModel, null));
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
            return ConvertModel(reservation, null);
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
        /// リポジトリモデル→ドメインモデルの変換
        /// </summary>
        /// <param name="reservations"></param>
        /// <returns></returns>
        private static ReservationDomainModel ConvertModel(ReservationRepositoryModel reservation, DoctorInfoRepositoryModel? doctorInfo)
            => new ReservationDomainModel(
                reservation.ReservationId,
                reservation.DoctorId,
                reservation.TargetDateTime,
                reservation.PatientId,
                reservation.ReservationDateTime);
    }
}

