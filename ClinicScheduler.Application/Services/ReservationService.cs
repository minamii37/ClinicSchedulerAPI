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

        public ReservationViewModel PostReservationService(ReservationViewModel requestView)
        {
            var requestModel = new ReservationViewModel().Transfer(requestView);
            //var duplicateModels = _reservateRepository.GetRelatedExistingReservation(requestModel);
            //duplicateModels.First().CheckDupulicate(duplicateModels, requestModel);
            requestModel = _reservateRepository.PostReservation(requestModel);
            return new ReservationViewModel().Presenter(requestModel);
        }
    }
}

