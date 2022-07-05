using System;
using ClinicScheduler.Application.ViewModels;
using ClinicScheduler.Domain.IRepositories;
using ClinicScheduler.Domain.Models;

namespace ClinicScheduler.Application.IServices
{
    public class ReservationService : IReservateService
    {
        private readonly IReservateRepository _reservateRepository;

        public ReservationService (IReservateRepository reservateRepository)
        {
            _reservateRepository = reservateRepository;
        }

        public ReservationViewModel PostReservationService(ReservationViewModel request)
        {
            var model = new ReservationViewModel().ReservationTransfer(request);
            model = _reservateRepository.PostReservation(model);
            return new ReservationViewModel().Presenter(model);
        }
    }
}

