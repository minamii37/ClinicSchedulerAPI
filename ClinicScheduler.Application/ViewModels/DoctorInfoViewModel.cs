using System;
using ClinicScheduler.Domain.Models.DoctorInfoDomainModel;

namespace ClinicScheduler.Application.ViewModels
{
    public class DoctorInfoViewModel
    {
        /// <summary>
        /// 患者ID
        /// </summary>
        public string DoctorId { get; set; }
        /// <summary>
        /// 患者名
        /// </summary>
        public string DoctorName { get; set; }
        /// <summary>
        /// 登録日時
        /// </summary>
        public DateTime? CreateDateTime { get; set; }

        public DoctorInfoViewModel()
        {
        }

        public DoctorInfoViewModel Presenter(DoctorInfoDomainModel model)
            => new DoctorInfoViewModel()
            {
                DoctorId = model.DoctorId,
                DoctorName = model.DoctorName,
                CreateDateTime = model.CreateDateTime
            };

        public DoctorInfoDomainModel Transfer(DoctorInfoViewModel view)
            => new DoctorInfoDomainModel(
                view.DoctorId,
                view.DoctorName,
                null);
    }
}