﻿using System;
namespace ClinicScheduler.Infrastructure.Models
{
    public class ScheduleRepositoryModel : BaseRepositoryModel
    {
        /// <summary>
        /// ドクターID
        /// </summary>
        public string DoctorId { get; set; }
        /// <summary>
        /// 対象日時
        /// </summary>
        public DateTime TargetDateTime { get; set; }
        /// <summary>
        /// 予約空き状況
        /// </summary>
        public bool IsEmpty { get; set; }
        /// <summary>
        /// 患者ID
        /// </summary>
        public string? PatientId { get; set; }
        /// <summary>
        /// 予約日時
        /// </summary>
        public DateTime? ReservationDateTime { get; set; }
        /// <summary>
        /// 承認者ID
        /// </summary>
        public string? ApprovalId { get; set; }
        /// <summary>
        /// 承認日時
        /// </summary>
        public DateTime? ApprovalDateTime { get; set; }
    }
}

