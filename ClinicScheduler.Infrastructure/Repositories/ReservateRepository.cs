using System;
using System.Net.Http;
using ClinicScheduler.Domain.Models;
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

        public IEnumerable<ReservationDomainModel> GetRelatedExistingReservation(ReservationDomainModel request)
        {
            // 関連既存予約の取得
            var repositoryModels = GetGetRelatedExistingReservationFromDb(request);

            var domainmodels = new List<ReservationDomainModel>();
            foreach (var repositoryModel in repositoryModels)
            {
                domainmodels.Add(ConvertModel(repositoryModel, null));
            }

            return domainmodels;
        }

        public ReservationDomainModel PostReservation(ReservationDomainModel request)
        {
            // JSONファイル書き込みのため、全データ取得
            var reservations = GetAllReservations();

            // 追加予約データの作成
            var requestModel = new ReservationRepositoryModel
            {
                ReservationId = Guid.NewGuid().ToString(),
                DoctorId = request.DoctorId,
                TargetDateTime = request.TargetDateTime,
                PatientId = request.PatientId,
                ReservationDateTime = DateTime.Now
            };

            // 取得データに予約データを追加し、書き込みの実施
            reservations = reservations?.Append(requestModel);
            string json = JsonConvert.SerializeObject(reservations, Formatting.Indented);
            File.WriteAllText(@$"{ directoryPath}/ReservationTable.json", json);

            return request;
        }

        /// <summary>
        /// 予約テーブルデータの全取得
        /// </summary>
        /// <returns></returns>
        private IEnumerable<ReservationRepositoryModel> GetAllReservations()
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
        /// 関連既存予約データの取得
        /// </summary>
        /// <returns>自予約</returns>
        private IEnumerable<ReservationRepositoryModel> GetGetRelatedExistingReservationFromDb(ReservationDomainModel request)
        {
            var repositoryModels = GetAllReservations();

            // 対象の日時か、患者IDとドクターIDで未完了の予約を絞り込み
            repositoryModels = repositoryModels?.Where(
                x => x.TargetDateTime == request.TargetDateTime
                    || (request.PatientId!.Equals(x.PatientId)
                    && request.DoctorId.Equals(x.DoctorId)
                    && x.TargetDateTime < DateTime.Now));

            return repositoryModels ?? Enumerable.Empty<ReservationRepositoryModel>();
        }

        /// <summary>
        /// リポジトリモデル→ドメインモデルの変換
        /// </summary>
        /// <param name="reservations"></param>
        /// <returns></returns>
        private ReservationDomainModel ConvertModel(ReservationRepositoryModel reservation, DoctorInfoRepositoryModel? doctorInfo)
            => new ReservationDomainModel(
                reservation.DoctorId,
                doctorInfo?.DoctorName,
                reservation.TargetDateTime,
                reservation.PatientId,
                null,
                reservation.ReservationDateTime);
    }
}

