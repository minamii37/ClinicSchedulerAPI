using System;
namespace ClinicScheduler.Domain.Models
{
    public class ReservationDomainModel : BaseDomainModel
    {
        /// <summary>
        /// 予約可否
        /// </summary>
        public bool CanReserve { get; set; } = true;
        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public string? ErrorMessage { get; set; }

        public ReservationDomainModel()
        {

        }
    }
}

