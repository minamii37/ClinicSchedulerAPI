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
        private readonly string directoryPath = "/Users/minami/Projects/ClinicSchedulerAPI/ClinicScheduler.nfrastructure/Data";

        public ReservateRepository()
        {
        }

        public ReservationDomainModel PostReservation(ReservationDomainModel request)
        {
            var reservations = GetOwnReservations(request.PatientId!, request.DoctorId);

            // 患者IDとドクターIDで未完了の予約を絞り込み
            // 本来は呼び出しメソッド内で行うが、DB接続の代わりにJSON読み書きを行うため一時的に以下で処理
            var repositoryModels = reservations?.Where(
                x => request.PatientId!.Equals(x.PatientId)
                    && request.DoctorId.Equals(x.DoctorId)
                    && x.TargetDateTime < DateTime.Now)
                ?? Enumerable.Empty<ReservationRepositoryModel>();

            if (repositoryModels.Any())
            {
                // 予約情報が取得できた場合は予約不可
                request.CanReserve = false;
                request.ErrorMessage = "対象のドクターには既に予約済みのため、現在新たな予約はできません";

                return request;
            }

            // 予約実行（JSONファイルへの書き込み）
            var requestModel = new ReservationRepositoryModel
            {
                ReservationId = Guid.NewGuid().ToString(),
                DoctorId = request.DoctorId,
                TargetDateTime = request.TargetDateTime,
                PatientId = request.PatientId,
                ReservationDateTime = DateTime.Now
            };
            reservations = reservations?.Append(requestModel);

            string json = JsonConvert.SerializeObject(reservations, Formatting.Indented);
            File.WriteAllText(@$"{ directoryPath}/ReservationTable.json", json);

            return request;
        }

        /// <summary>
        /// 予約状況の確認
        /// </summary>
        /// <returns></returns>
        private IEnumerable<ReservationRepositoryModel> GetOwnReservations(string patientId, string doctorId)
        {
            // JSONデータの取得
            StreamReader r = new StreamReader($"{directoryPath}/ReservationTable.json");
            string jsonString = r.ReadToEnd();
            // JSONデータのデシリアライズ
            IEnumerable<ReservationRepositoryModel>? repositoryModels =
                JsonConvert.DeserializeObject<IEnumerable<ReservationRepositoryModel>>(jsonString);

            //// 患者IDとドクターIDで未完了の予約を絞り込み
            //repositoryModels = repositoryModels?.Where(
            //    x => patientId.Equals(x.PatientId)
            //        && doctorId.Equals(x.DoctorId)
            //        && x.TargetDateTime < DateTime.Now);

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

            // ドクターIDで絞り込み
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
        private IEnumerable<ScheduleDomainModel> ConvertModels(
            IEnumerable<ReservationRepositoryModel> reservations, IEnumerable<DoctorInfoRepositoryModel> doctorInfomations)
        {
            IEnumerable<ScheduleDomainModel> domainModels = Enumerable.Empty<ScheduleDomainModel>();

            foreach(var doctorInfo in doctorInfomations)
            {
                foreach (var reservation in reservations.Where(x => x.DoctorId.Equals(doctorInfo.DoctorId)))
                {
                    var domainModel = new ScheduleDomainModel()
                    {
                        DoctorId = doctorInfo.DoctorId,
                        DoctorName = doctorInfo.DoctorName,
                        TargetDateTime = reservation.TargetDateTime,
                        PatientId = reservation.PatientId,
                        ReservationDateTime = reservation.ReservationDateTime,
                        ApprovalId = reservation.ApprovalId,
                        ApprovalDateTime = reservation.ApprovalDateTime,
                    };

                    domainModels = domainModels.Append(domainModel);
                }
            }

            return domainModels;
        }
    }
}

