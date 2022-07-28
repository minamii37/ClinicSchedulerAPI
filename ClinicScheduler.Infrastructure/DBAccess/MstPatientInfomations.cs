using System;
using ClinicScheduler.Infrastructure.Models;
using Newtonsoft.Json;

namespace ClinicScheduler.Infrastructure.DBAccess
{
    public class MstPatientInfomations
    {
        private readonly string directoryPath = "../ClinicScheduler.Infrastructure/Data";

        public MstPatientInfomations()
        {
        }

        /// <summary>
        /// 患者テーブルデータの全取得
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PatientInfoRepositoryModel> GetAllPatientInfoFromDB()
        {
            // JSONデータの取得
            StreamReader r = new StreamReader($"{directoryPath}/PatientInfoTable.json");
            string jsonString = r.ReadToEnd();
            // JSONデータのデシリアライズ
            IEnumerable<PatientInfoRepositoryModel>? repositoryModels =
                JsonConvert.DeserializeObject<IEnumerable<PatientInfoRepositoryModel>>(jsonString);

            return repositoryModels ?? Enumerable.Empty<PatientInfoRepositoryModel>();
        }

        public void PostNewPatientInfoToDB(IEnumerable<PatientInfoRepositoryModel> registDataList)
        {
            string json = JsonConvert.SerializeObject(registDataList, Formatting.Indented);
            File.WriteAllText(@$"{ directoryPath}/PatientInfoTable.json", json);
        }
    }
}

