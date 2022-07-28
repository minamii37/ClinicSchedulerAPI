using System;
using ClinicScheduler.Domain.Models.DoctorInfoDomainModel;
using ClinicScheduler.Infrastructure.Models;
using Newtonsoft.Json;

namespace ClinicScheduler.Infrastructure.DBAccess
{
    public class MstDoctorInfomations
    {
        private readonly string directoryPath = "/Users/minami/Projects/ClinicSchedulerAPI/ClinicScheduler.Infrastructure/Data";

        public MstDoctorInfomations()
        {
        }

        /// <summary>
        /// ドクター情報の全取得
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DoctorInfoRepositoryModel> GetAllDoctorInfomationsFromDB()
        {
            // JSONデータの取得
            StreamReader r = new StreamReader($"{directoryPath}/DoctorInfoTable.json");
            string jsonString = r.ReadToEnd();
            // JSONデータのデシリアライズ
            IEnumerable<DoctorInfoRepositoryModel>? repositoryModels =
                JsonConvert.DeserializeObject<IEnumerable<DoctorInfoRepositoryModel>>(jsonString);

            return repositoryModels ?? Enumerable.Empty<DoctorInfoRepositoryModel>();
        }

        /// <summary>
        /// 指定ドクター情報の取得
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DoctorInfoRepositoryModel> GetTargetDoctorInfomationsFromDB(IEnumerable<string> doctorIdList)
        {
            var repositoryModels = GetAllDoctorInfomationsFromDB();
            repositoryModels = repositoryModels?.Where(x => doctorIdList.Contains(x.DoctorId));

            return repositoryModels ?? Enumerable.Empty<DoctorInfoRepositoryModel>();
        }

        /// <summary>
        /// テーブルデータの書き込み（上書き）処理
        /// </summary>
        /// <param name="registDataList"></param>
        public void PostNewDoctorInfoToDB(IEnumerable<DoctorInfoRepositoryModel> registDataList)
        {
            string json = JsonConvert.SerializeObject(registDataList, Formatting.Indented);
            File.WriteAllText(@$"{ directoryPath}/DoctorInfoTable.json", json);
        }
    }
}

