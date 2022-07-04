using System;
namespace ClinicScheduler.Domain.Models
{
    public class ScheduleDomainModel : BaseDomainModel
    {
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

