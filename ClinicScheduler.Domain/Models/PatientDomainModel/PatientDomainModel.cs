using System;
namespace ClinicScheduler.Domain.Models.PatientDomainModel
{
    public class PatientDomainModel
    {
        /// <summary>
        /// 患者ID
        /// </summary>
        public string PatientId { get; private set; }
        /// <summary>
        /// 患者氏名
        /// </summary>
        public string PatientName { get; private set; }
        /// <summary>
        /// 登録日時
        /// </summary>
        public DateTime? CreateDateTime { get; private set; }

        public PatientDomainModel(string patientId, string patientName, DateTime? createDateTime)
        {
            PatientId = patientId;
            PatientName = patientName;
            CreateDateTime = createDateTime;
        }
    }
}

