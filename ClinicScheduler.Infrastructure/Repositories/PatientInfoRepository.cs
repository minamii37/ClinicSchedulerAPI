using System;
using System.Net.Http;
using ClinicScheduler.Domain.Models.PatientInfoDomainModel;
using ClinicScheduler.Domain.IRepositories;
using ClinicScheduler.Infrastructure.Models;
using Newtonsoft.Json;
using System.Net;
using ClinicScheduler.Infrastructure.DBAccess;

namespace ClinicScheduler.Infrastructure.Repositories
{
    public class PatientInfoRepository : IPatientInfoRepository
    {
        public PatientInfoRepository()
        {
        }

        public IEnumerable<PatientInfoDomainModel> GetPatientList()
        {
            var repositoryModels = new MstPatientInfomations().GetAllPatientInfoFromDB();

            var domainModels = new List<PatientInfoDomainModel>();
            foreach (var repositoryModel in repositoryModels)
            {
                var domainModel = ConvertModel(repositoryModel);
                domainModels.Add(domainModel);
            }
            return domainModels;
        }

        public IEnumerable<PatientInfoDomainModel> GetTargetPatientList(IEnumerable<string> patientIds)
        {
            var repositoryModels = new MstPatientInfomations()
                .GetAllPatientInfoFromDB().Where(x => patientIds.Contains(x.PatientId));

            var domainModels = new List<PatientInfoDomainModel>();
            foreach (var repositoryModel in repositoryModels)
            {
                var domainModel = ConvertModel(repositoryModel);
                domainModels.Add(domainModel);
            }
            return domainModels;
        }

        public PatientInfoDomainModel PostNewPatientInfo(PatientInfoDomainModel request)
        {
            // JSONファイル書き込みのため、全データ取得
            var mstPatientInfomations = new MstPatientInfomations();
            var patientInfomations = mstPatientInfomations.GetAllPatientInfoFromDB();

            // 追加新規データの作成
            var requestModel = new PatientInfoRepositoryModel
            {
                PatientId = Guid.NewGuid().ToString(),
                PatientName = request.PatientName,
                CreateDate = DateTime.Now
            };

            // 取得データに新規データを追加し、書き込みの実施
            patientInfomations = patientInfomations.Append(requestModel);
            mstPatientInfomations.PostNewPatientInfoToDB(patientInfomations);

            return ConvertModel(requestModel);
        }

        /// <summary>
        /// リポジトリモデル→ドメインモデルの変換
        /// </summary>
        /// <returns></returns>
        private PatientInfoDomainModel ConvertModel(PatientInfoRepositoryModel patientInfo)
            => new PatientInfoDomainModel(patientInfo.PatientId, patientInfo.PatientName, patientInfo.CreateDate);
    }
}

