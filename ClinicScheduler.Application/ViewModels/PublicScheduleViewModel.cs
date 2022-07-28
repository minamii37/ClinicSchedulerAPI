using System;
using ClinicScheduler.Domain.Models.ScheduleDomainModel;
using ClinicScheduler.Domain.Models.ScheduleDomainModel.ValueObjects;

namespace ClinicScheduler.Application.ViewModels
{
    public class PublicScheduleViewModel
    {
        /// <summary>
        /// 医師ID
        /// </summary>
        public string DoctorId { get; set; }
        /// <summary>
        /// 医師名
        /// </summary>
        public string DoctorName { get; set; }
        /// <summary>
        /// 対象日時
        /// </summary>
        public DateTime TargetDateTime { get; set; }

        public PublicScheduleViewModel()
        {
        }

        public PublicScheduleViewModel Presenter(ScheduleDomainModel schedule, DoctorInfoModel doctorInfo)
            => new PublicScheduleViewModel()
            {
                DoctorId = doctorInfo.DoctorId,
                DoctorName = doctorInfo.DoctorName,
                TargetDateTime = schedule.TargetDateTime,
            };
    }
}


