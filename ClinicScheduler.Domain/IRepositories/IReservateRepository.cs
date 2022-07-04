using System;
using ClinicScheduler.Domain.Models;

namespace ClinicScheduler.Domain.IRepositories
{
    public interface IReservateRepository
    {
        /// <summary>
        /// 予約の実施
        /// </summary>
        /// <returns>予約成否</returns>
        ReservationDomainModel PostReservation(ReservationDomainModel model);
    }
}

