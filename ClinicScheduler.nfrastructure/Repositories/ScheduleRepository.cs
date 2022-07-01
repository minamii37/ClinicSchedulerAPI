using System;
using ClinicScheduler.Domain.Models;
using ClinicScheduler.Domain.Repositories;
using ClinicScheduler.Infrastructure.Models;
using Newtonsoft.Json;

namespace ClinicScheduler.Infrastructure.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        public ScheduleRepository()
        {
        }

        public IEnumerable<ScheduleDomainModel> GetSchedule()
        {
            var repositoryModels = GetScheduleRepository();
            return ConvertModels(repositoryModels);
        }

        public IEnumerable<ScheduleDomainModel> GetScheduleByDoctorId(string doctorId)
        {
            var repositoryModels = GetScheduleRepository().Where(x => x.DoctorId.Equals(doctorId));
            return ConvertModels(repositoryModels);
        }

        /// <summary>
        /// スケジュールテーブルデータの取得
        /// </summary>
        /// <returns></returns>
        private IEnumerable<ScheduleRepositoryModel> GetScheduleRepository()
        {
            // JSONデータの取得
            StreamReader r = new StreamReader("./Data/ScheduleTable.json");
            string jsonString = r.ReadToEnd();
            // JSONデータのデシリアライズ
            IEnumerable<ScheduleRepositoryModel>? scheduleRepositoryModels =
                JsonConvert.DeserializeObject<IEnumerable<ScheduleRepositoryModel>>(jsonString);

            return scheduleRepositoryModels ?? Enumerable.Empty<ScheduleRepositoryModel>();
        }

        /// <summary>
        /// リポジトリモデル→ドメインモデルの変換
        /// </summary>
        /// <param name="repositoryModels"></param>
        /// <returns></returns>
        private IEnumerable<ScheduleDomainModel> ConvertModels(IEnumerable<ScheduleRepositoryModel> repositoryModels)
        {
            IEnumerable<ScheduleDomainModel> domainModels = Enumerable.Empty<ScheduleDomainModel>();
            foreach (var model in repositoryModels)
            {
                var domainModel = new ScheduleDomainModel()
                {
                    DoctorId = model.DoctorId,
                    TargetDateTime = model.TargetDateTime,
                    IsEmpty = model.IsEmpty,
                    PatientId = model.PatientId,
                    ReservationDateTime = model.ReservationDateTime,
                    ApprovalId = model.ApprovalId,
                    ApprovalDateTime = model.ApprovalDateTime,
                    CreateDateTime = model.CreateDateTime,
                    LastUpdateTime = model.LastUpdateTime,
                    LastUpdateId = model.LastUpdateId
                };

                domainModels.Append(domainModel);
            }

            return domainModels;
        }
    }
}

