using System;
using System.Net.Http;
using ClinicScheduler.Domain.Models.DoctorInfoDomainModel;
using ClinicScheduler.Domain.IRepositories;
using ClinicScheduler.Infrastructure.Models;
using Newtonsoft.Json;
using System.Net;
using ClinicScheduler.Infrastructure.DBAccess;

namespace ClinicScheduler.Infrastructure.Repositories
{
    public class DoctorInfoRepository : IDoctorInfoRepository
    {
        public DoctorInfoRepository()
        {
        }

        public IEnumerable<DoctorInfoDomainModel> GetDoctorList()
        {
            var repositoryModels = new MstDoctorInfomations().GetAllDoctorInfomationsFromDB();

            var domainModels = new List<DoctorInfoDomainModel>();
            foreach (var repositoryModel in repositoryModels)
            {
                var domainModel = ConvertModel(repositoryModel);
                domainModels.Add(domainModel);
            }
            return domainModels;
        }

        public DoctorInfoDomainModel PostNewDoctorInfo(DoctorInfoDomainModel request)
        {
            // JSONファイル書き込みのため、全データ取得
            var mstDoctorInfomations = new MstDoctorInfomations();
            var doctorInfomations = mstDoctorInfomations.GetAllDoctorInfomationsFromDB();

            // 追加新規データの作成
            var requestModel = new DoctorInfoRepositoryModel
            {
                DoctorId = Guid.NewGuid().ToString(),
                DoctorName = request.DoctorName,
                CreateDate = DateTime.Now
            };

            // 取得データに新規データを追加し、書き込みの実施
            doctorInfomations = doctorInfomations.Append(requestModel);
            mstDoctorInfomations.PostNewDoctorInfoToDB(doctorInfomations);

            return ConvertModel(requestModel);
        }

        /// <summary>
        /// リポジトリモデル→ドメインモデルの変換
        /// </summary>
        /// <returns></returns>
        private DoctorInfoDomainModel ConvertModel(DoctorInfoRepositoryModel doctorInfo)
            => new DoctorInfoDomainModel(doctorInfo.DoctorId, doctorInfo.DoctorName, doctorInfo.CreateDate);
    }
}

