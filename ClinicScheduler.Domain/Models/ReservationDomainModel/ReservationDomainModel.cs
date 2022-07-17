using System;
namespace ClinicScheduler.Domain.Models.ReservationDomainModel
{
    public class ReservationDomainModel
    {
        /// <summary>
        /// 予約ID
        /// </summary>
        public string ReservationId { get; private set; }
        /// <summary>
        /// ドクターID
        /// </summary>
        public string DoctorId { get; private set; }
        /// <summary>
        /// 対象日時
        /// </summary>
        public DateTime TargetDateTime { get; private set; }
        /// <summary>
        /// 患者ID
        /// </summary>
        public string PatientId { get; private set; }
        /// <summary>
        /// 予約日時
        /// </summary>
        public DateTime? ReservationDateTime { get; private set; }
        
        public ReservationDomainModel(
            string reservationId, string doctorId, DateTime targetDateTime, string patientId, DateTime? reservationDateTime)
        {
            ReservationId = reservationId;
            DoctorId = doctorId;
            TargetDateTime = targetDateTime;
            PatientId = patientId;
            ReservationDateTime = reservationDateTime;
        }

        public ReservationDomainModel PostReservation(string doctorId, string patientId)
        {
            if (!String.IsNullOrEmpty(ReservationId))
            {
                throw new InvalidOperationException("指定日時は予約済みのため、予約できません");
            }

            if (TargetDateTime.Date <= DateTime.Now.Date)
            {
                throw new InvalidOperationException("指定日の予約受付は終了しました。翌日以降のみ予約可能です");
            }

            if (TargetDateTime.DayOfWeek == DayOfWeek.Sunday)
            {
                throw new InvalidOperationException("日曜日は休診日です");
            }

            if (TargetDateTime.DayOfWeek == DayOfWeek.Saturday
                && TargetDateTime.Hour >= 13)
            {
                throw new InvalidOperationException("土曜の午後は休診です");
            }

            // 9:00〜13:00, 15:00〜19:00の間は予約可能
            if (TargetDateTime.Hour < 9
                || (TargetDateTime.Hour >= 13 && TargetDateTime.Hour < 15)
                || TargetDateTime.Hour >= 19)
            {
                throw new InvalidOperationException("受付時間外です");
            }

            ReservationId = new Guid().ToString();
            DoctorId = doctorId;
            PatientId = patientId;
            ReservationDateTime = DateTime.Now;

            return this;
        }
    }

}

