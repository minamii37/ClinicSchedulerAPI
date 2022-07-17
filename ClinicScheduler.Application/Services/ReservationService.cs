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

        public IEnumerable<ReservationViewModel> GetOwnReservationsService(string patientId)
        {
            var models = _reservateRepository.GetOwnReservations(patientId);
            var views = new List<ReservationViewModel>();
            foreach (var model in models)
            {
                var view = new ReservationViewModel().Presenter(model);
                views.Add(view);
            }
            return views;
        }

        public ReservationViewModel PostReservationService(ReservationViewModel requestView)
        {
            if ((requestView.TargetDateTime.Minute != 0
                    && requestView.TargetDateTime.Minute != 30)
                || requestView.TargetDateTime.Second != 0
                || requestView.TargetDateTime.Millisecond != 0)
            {
                // 30分刻みかつ、秒数が0秒ではないものは不正なリクエストとして扱う
                throw new InvalidOperationException("時刻の形式が不正です");
            }

            var reservation = _reservateRepository.GetSpecifiedDateReservation(requestView.TargetDateTime);
            var request = reservation.PostReservation(requestView.DoctorId, requestView.PatientId!);
            _reservateRepository.PostReservation(request);

            // 医師名と患者名ってどうやって撮るのが正解？？
            return new ReservationViewModel().Presenter(request);
        }
    }
}

