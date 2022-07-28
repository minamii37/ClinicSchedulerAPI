using System;
using ClinicScheduler.Application.ViewModels;

namespace ClinicScheduler.Application.IServices
{
    public interface IPrivateScheduleService
    {
        /// <summary>
        /// 指定日の非公開スケジュールの取得
        /// </summary>
        /// <returns>スケジュール</returns>
        IEnumerable<PrivateScheduleViewModel> GetPrivateScheduleForTheSpecifiedDateService(DateTime specifiedDate);

        /// <summary>
        /// 医師別非公開スケジュールの取得
        /// </summary>
        /// <param name="doctorId">医師ID</param>
        /// <returns>スケジュール</returns>
        IEnumerable<PrivateScheduleViewModel> GetDoctorPrivateScheduleService(string doctorId);

        /// <summary>
        /// 医師別非公開スケジュールの取得（開始日指定）
        /// </summary>
        /// <param name="doctorId">医師ID</param>
        /// <param name="startDate">開始日</param>
        /// <returns>スケジュール</returns>
        IEnumerable<PrivateScheduleViewModel> GetDoctorPrivateScheduleForTheSpecifiedWeekService(string doctorId, DateTime startDate);
    }
}

