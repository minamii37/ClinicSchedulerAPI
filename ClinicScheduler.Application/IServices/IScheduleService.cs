using System;
using ClinicScheduler.Application.ViewModels;

namespace ClinicScheduler.Application.IServices
{
    public interface IScheduleService
    {
        /// <summary>
        /// スケジュールの取得
        /// </summary>
        /// <returns>スケジュール</returns>
        IEnumerable<ScheduleViewModel> GetScheduleService();
    }
}

