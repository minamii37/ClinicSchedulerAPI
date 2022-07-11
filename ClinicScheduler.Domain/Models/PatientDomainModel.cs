using System;
namespace ClinicScheduler.Domain.Models
{
    public class PatientDomainModel
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
        public DateTime? CreateDateTime { get; set; }

        public PatientDomainModel(string patientId, string patientName, DateTime? createDateTime)
        {
            PatientId = patientId;
            PatientName = patientName;
            CreateDateTime = createDateTime;
        }
    }
}

