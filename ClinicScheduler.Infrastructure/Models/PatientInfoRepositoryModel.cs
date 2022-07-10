using System;
namespace ClinicScheduler.Infrastructure.Models
{
    public class PatientInfoRepositoryModel
    {
        /// <summary>
        /// 患者ID
        /// </summary>
        public string PatientId { get; set; }
        /// <summary>
        /// 患者氏名
        /// </summary>
        public string PatientName { get; set; }
        /// <summary>
        /// 登録日時
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}

