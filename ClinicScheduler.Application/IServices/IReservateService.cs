using System;
using ClinicScheduler.Application.ViewModels;

namespace ClinicScheduler.Application.IServices
{
    public interface IReservateService
    {
        /// <summary>
        /// 予約の実施
        /// </summary>
        /// <returns>スケジュール</returns>
        PublicScheduleViewModel PostReservationService(PublicScheduleViewModel request);
    }
}

