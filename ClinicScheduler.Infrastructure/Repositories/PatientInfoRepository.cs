﻿using System;
using System.Net.Http;
using ClinicScheduler.Domain.Models;
using ClinicScheduler.Domain.IRepositories;
using ClinicScheduler.Infrastructure.Models;
using Newtonsoft.Json;
using System.Net;

namespace ClinicScheduler.Infrastructure.Repositories
{
    public class PatientRepository : IPatientInfoRepository
    {
        private readonly string directoryPath = "../ClinicScheduler.Infrastructure/Data";

        public PatientRepository()
        {
        }

        public IEnumerable<PatientDomainModel> GetPatientList()
        {
            var repositoryModels = GetAllPatientInfoFromDB();
            var domainModels = new List<PatientDomainModel>();
            foreach (var repositoryModel in repositoryModels)
            {
                var domainModel = ConvertModel(repositoryModel);
                domainModels.Add(domainModel);
            }
            return domainModels;
        }

        public PatientDomainModel PostNewPatientInfo(PatientDomainModel request)
        {
            // JSONファイル書き込みのため、全データ取得
            var patientInfomations = GetAllPatientInfoFromDB();

            // 追加新規データの作成
            var requestModel = new PatientInfoRepositoryModel
            {
                PatientId = Guid.NewGuid().ToString(),
                PatientName = request.PatientName,
                CreateDate = DateTime.Now
            };

            // 取得データに新規データを追加し、書き込みの実施
            patientInfomations = patientInfomations?.Append(requestModel);
            string json = JsonConvert.SerializeObject(patientInfomations, Formatting.Indented);
            File.WriteAllText(@$"{ directoryPath}/PatientInfoTable.json", json);

            return ConvertModel(requestModel);
        }

        /// <summary>
        /// 患者テーブルデータの全取得
        /// </summary>
        /// <returns></returns>
        private IEnumerable<PatientInfoRepositoryModel> GetAllPatientInfoFromDB()
        {
            // JSONデータの取得
            StreamReader r = new StreamReader($"{directoryPath}/PatientInfoTable.json");
            string jsonString = r.ReadToEnd();
            // JSONデータのデシリアライズ
            IEnumerable<PatientInfoRepositoryModel>? repositoryModels =
                JsonConvert.DeserializeObject<IEnumerable<PatientInfoRepositoryModel>>(jsonString);

            return repositoryModels ?? Enumerable.Empty<PatientInfoRepositoryModel>();
        }

        /// <summary>
        /// リポジトリモデル→ドメインモデルの変換
        /// </summary>
        /// <returns></returns>
        private PatientDomainModel ConvertModel(PatientInfoRepositoryModel patientInfo)
            => new PatientDomainModel(patientInfo.PatientId, patientInfo.PatientName, patientInfo.CreateDate);
    }
}
