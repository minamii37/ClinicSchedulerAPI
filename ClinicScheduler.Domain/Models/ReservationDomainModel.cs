﻿using System;
namespace ClinicScheduler.Domain.Models
{
    public class ReservationDomainModel
    {
        /// <summary>
        /// ドクターID
        /// </summary>
        public string DoctorId { get; set; }
        /// <summary>
        /// ドクター氏名
        /// </summary>
        public string? DoctorName { get; set; }
        /// <summary>
        /// 対象日時
        /// </summary>
        public DateTime TargetDateTime { get; set; }
        /// <summary>
        /// 患者ID
        /// </summary>
        public string? PatientId { get; set; }
        /// <summary>
        /// 患者氏名
        /// </summary>
        public string? PatientName { get; set; }
        /// <summary>
        /// 予約日時
        /// </summary>
        public DateTime? ReservationDateTime { get; set; }

        public ReservationDomainModel(
            string doctorId, string? doctorName, DateTime targetDateTime, string? patientId, string? patientName, DateTime? reservationDateTime)
        {
            DoctorId = doctorId;
            DoctorName = doctorName;
            TargetDateTime = targetDateTime;
            PatientId = patientId;
            PatientName = patientName;
            ReservationDateTime = reservationDateTime;
        }

        public void CheckDupulicate(IEnumerable<ReservationDomainModel> duplicateModels, ReservationDomainModel request)
        {
            if (!duplicateModels.Any())
            {
                return;
            }

            //var hitModels = duplicateModels.Where(x => x.TargetDateTime)
        }
    }
}

