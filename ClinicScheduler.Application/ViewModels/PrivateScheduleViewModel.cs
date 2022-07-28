using System;
using ClinicScheduler.Domain.Models.ScheduleDomainModel;
using ClinicScheduler.Domain.Models.ScheduleDomainModel.ValueObjects;

namespace ClinicScheduler.Application.ViewModels
{
    public class PrivateScheduleViewModel
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
        /// <summary>
        /// 患者ID
        /// </summary>
        public string PatientId { get; set; }
        /// <summary>
        /// 患者名
        /// </summary>
        public string PatientName { get; set; }

        public PrivateScheduleViewModel()
        {
        }

        public PrivateScheduleViewModel Presenter(
            ScheduleDomainModel schedule, DoctorInfoModel doctorInfo, PatientInfoModel patientInfo)
            => new PrivateScheduleViewModel()
            {
                DoctorId = doctorInfo.DoctorId,
                DoctorName = doctorInfo.DoctorName,
                TargetDateTime = schedule.TargetDateTime,
                PatientId = patientInfo.PatientId,
                PatientName = patientInfo.PatientName
            };
    }
}


