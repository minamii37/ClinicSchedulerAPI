using System;
namespace ClinicScheduler.Infrastructure.Models
{
    public class DoctorInfoRepositoryModel
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
        /// 作成日時
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}

