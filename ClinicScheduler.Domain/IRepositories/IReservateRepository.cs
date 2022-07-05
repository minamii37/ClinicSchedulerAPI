using System;
using ClinicScheduler.Domain.Models;

namespace ClinicScheduler.Domain.IRepositories
{
    public interface IReservateRepository
    {
        /// <summary>
        /// 既存の関連する予約の取得
        /// </summary>
        /// <param name="patientId">患者ID</param>
        /// <param name="doctorId">医師ID</param>
        /// <returns>既存の自予約（存在しない場合はnull）</returns>
        IEnumerable<ReservationDomainModel> GetRelatedExistingReservation(ReservationDomainModel request);

        /// <summary>
        /// 予約の実施
        /// </summary>
        /// <returns>予約成否</returns>
        ReservationDomainModel PostReservation(ReservationDomainModel model);
    }
}

