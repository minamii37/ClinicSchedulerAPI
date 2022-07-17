using System;
using ClinicScheduler.Domain.Models.ReservationDomainModel;

namespace ClinicScheduler.Application.ViewModels
{
    public class ReservationViewModel
    {
        /// <summary>
        /// 予約ID
        /// </summary>
        public string ReservationId { get; set; }
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
        /// <summary>
        /// 予約日時
        /// </summary>
        public DateTime? ReservationDateTime { get; set; }

        public ReservationViewModel()
        {
        }

        public ReservationViewModel Presenter(ReservationDomainModel model)
            => new ReservationViewModel()
            {
                ReservationId = model.ReservationId,
                DoctorId = model.DoctorId,
                //DoctorName = model.DoctorName!,
                TargetDateTime = model.TargetDateTime,
                PatientId = model.PatientId,
                //PatientName = model.PatientName,
                ReservationDateTime = model.ReservationDateTime
            };
    }
}