using System;
using ClinicScheduler.Infrastructure.Models;
using Newtonsoft.Json;

namespace ClinicScheduler.Infrastructure.DBAccess
{
    public class TrnReservations
    {
        private readonly string directoryPath = "/Users/minami/Projects/ClinicSchedulerAPI/ClinicScheduler.Infrastructure/Data";

        public TrnReservations()
        {
        }

        /// <summary>
        /// 予約テーブルデータの全取得
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ReservationRepositoryModel> GetAllReservations()
        {
            // JSONデータの取得
            StreamReader r = new StreamReader($"{directoryPath}/ReservationTable.json");
            string jsonString = r.ReadToEnd();
            // JSONデータのデシリアライズ
            IEnumerable<ReservationRepositoryModel>? repositoryModels =
                JsonConvert.DeserializeObject<IEnumerable<ReservationRepositoryModel>>(jsonString);

            return repositoryModels ?? Enumerable.Empty<ReservationRepositoryModel>();
        }
    }
}

