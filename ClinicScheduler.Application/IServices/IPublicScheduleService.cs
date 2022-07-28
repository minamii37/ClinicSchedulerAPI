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
        /// <param name="doctorId">医師ID</param>
        /// <returns>スケジュール</returns>
        IEnumerable<PublicScheduleViewModel> GetDoctorPublicScheduleService(string doctorId);

        /// <summary>
        /// 医師別公開スケジュールの取得（開始日指定）
        /// </summary>
        /// <param name="doctorId">医師ID</param>
        /// <param name="startDate">開始日</param>
        /// <returns>スケジュール</returns>
        IEnumerable<PublicScheduleViewModel> GetDoctorPublicScheduleForTheSpecifiedWeekService(string doctorId, DateTime startDate);
    }
}

