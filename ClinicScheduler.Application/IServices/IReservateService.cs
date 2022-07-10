using System;
using ClinicScheduler.Application.ViewModels;

namespace ClinicScheduler.Application.IServices
{
    public interface IReservateService
    {
        /// <summary>
        /// 自分の予約情報の取得
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns>自分の予約情報の取得</returns>
        IEnumerable<ReservationViewModel> GetOwnReservationsService(string patientId);

        /// <summary>
        /// 予約の実施
        /// </summary>
        /// <returns>スケジュール</returns>
        ReservationViewModel PostReservationService(ReservationViewModel request);
    }
}

