using System;
namespace ClinicScheduler.Domain.Models
{
    public class ScheduleDomainModel
    {
        /// <summary>
        /// ドクターID
        /// </summary>
        public string DoctorId { get; set; }
        /// <summary>
        /// ドクター氏名
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
        /// 患者氏名
        /// </summary>
        public string? PatientName { get; set; }
        /// <summary>
        /// 予約日時
        /// </summary>
        public DateTime? ReservationDateTime { get; set; }
        /// <summary>
        /// 承認者ID
        /// </summary>
        public string? ApprovalId { get; set; }
        /// <summary>
        /// 承認者氏名
        /// </summary>
        public string? ApprovalName { get; set; }
        /// <summary>
        /// 承認日時
        /// </summary>
        public DateTime? ApprovalDateTime { get; set; }

        public ScheduleDomainModel()
        {

        }
    }
}

