using System;
using ClinicScheduler.Domain.Models;

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
        /// <summary>
        /// 患者ID
        /// </summary>
        public string? PatientId { get; set; }
        /// <summary>
        /// 患者名
        /// </summary>
        public string? PatientName { get; set; }

        public PublicScheduleViewModel()
        {
        }

        public PublicScheduleViewModel PublicConverter(ScheduleDomainModel model)
            => new PublicScheduleViewModel()
            {
                DoctorId = model.DoctorId,
                DoctorName = model.DoctorName,
                TargetDateTime = model.TargetDateTime,
            };

        public PublicScheduleViewModel PrivateConverter(ScheduleDomainModel model)
            => new PublicScheduleViewModel()
            {
                DoctorId = model.DoctorId,
                DoctorName = model.DoctorName,
                TargetDateTime = model.TargetDateTime,
                PatientId = model.PatientId,
                PatientName = model.PatientName,
            };

    }
}

