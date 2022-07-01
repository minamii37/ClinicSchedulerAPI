using System;
using ClinicScheduler.Domain.Models;

namespace ClinicScheduler.Domain.Repositories
{
    public interface IScheduleRepository
    {
        /// <summary>
        /// 全スケジュールの取得
        /// </summary>
        /// <returns>全スケジュール</returns>
        IEnumerable<ScheduleDomainModel> GetSchedule();

        /// <summary>
        /// ドクター別スケジュールの取得
        /// </summary>
        /// <param name="doctorId">ドクターID</param>
        /// <returns>ドクター別スケジュール</returns>
        IEnumerable<ScheduleDomainModel> GetScheduleByDoctorId(string doctorId);
    }
}

