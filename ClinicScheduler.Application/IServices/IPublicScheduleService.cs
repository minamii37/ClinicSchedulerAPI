using System;
using ClinicScheduler.Application.ViewModels;

namespace ClinicScheduler.Application.IServices
{
    public interface IPublicScheduleService
    {
        /// <summary>
        /// 公開スケジュールの取得
        /// </summary>
        /// <returns>スケジュール</returns>
        IEnumerable<PublicScheduleViewModel> GetPublicScheduleService();

        /// <summary>
        /// 医師別公開スケジュールの取得
        /// </summary>
        /// <returns>スケジュール</returns>
        IEnumerable<PublicScheduleViewModel> GetPublicScheduleService(string doctorId);
    }
}

