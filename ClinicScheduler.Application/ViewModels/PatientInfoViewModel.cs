using System;
using ClinicScheduler.Domain.Models.PatientInfoDomainModel;

namespace ClinicScheduler.Application.ViewModels
{
    public class PatientInfoViewModel
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
        public DateTime? CreateDateTime { get; set; }

        public PatientInfoViewModel()
        {
        }

        public PatientInfoViewModel Presenter(PatientInfoDomainModel model)
            => new PatientInfoViewModel()
            {
                PatientId = model.PatientId,
                PatientName = model.PatientName,
                CreateDateTime = model.CreateDateTime
            };

        public PatientInfoDomainModel Transfer(PatientInfoViewModel view)
            => new PatientInfoDomainModel(
                view.PatientId,
                view.PatientName,
                null);
    }
}