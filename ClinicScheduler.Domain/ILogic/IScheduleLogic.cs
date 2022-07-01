using System;
using ClinicScheduler.Domain.Models;

namespace ClinicScheduler.Domain.ILogic
{
    public interface IScheduleLogic
    {
        /// <summary>
        /// スケジュールの取得
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns></returns>
        IEnumerable<ScheduleDomainModel> GetSchedule(string? doctorId);
    }
}

