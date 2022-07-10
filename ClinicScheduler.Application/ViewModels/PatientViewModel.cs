using System;
using ClinicScheduler.Domain.Models;

namespace ClinicScheduler.Application.ViewModels
{
    public class PatientViewModel
    {
        /// <summary>
        /// 患者ID
        /// </summary>
        public string PatientId { get; set; }
        /// <summary>
        /// 患者名
        /// </summary>
        public string PatientName { get; set; }
        /// <summary>
        /// 登録日時
        /// </summary>
        public DateTime CreateDateTime { get; set; }

        public PatientViewModel()
        {
        }

        public PatientViewModel Presenter(PatientDomainModel model)
            => new PatientViewModel()
            {
                PatientId = model.PatientId,
                PatientName = model.PatientName,
                CreateDateTime = model.CreateDateTime
            };
    }
}