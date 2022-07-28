using System;
using ClinicScheduler.Application.ViewModels;
using ClinicScheduler.Domain.IRepositories;
using ClinicScheduler.Domain.Models;

namespace ClinicScheduler.Application.IServices
{
    public class PublicScheduleService : IPublicScheduleService
    {
        private readonly IPublicScheduleRepository _publicScheduleRepository;

        public PublicScheduleService(IPublicScheduleRepository publicScheduleRepositpry)
        {
            _publicScheduleRepository = publicScheduleRepositpry;
        }

        public IEnumerable<PublicScheduleViewModel> GetPublicScheduleService()
        {
            var privateScheduleList = _publicScheduleRepository.GetPublicSchedule();
            var doctorInfos = _publicScheduleRepository.GetTargetDoctorList(privateScheduleList.Select(x => x.DoctorId));

            var views = new List<PublicScheduleViewModel>();
            foreach (var privateSchedule in privateScheduleList)
            {
                var doctorInfo = doctorInfos.First(x => x.DoctorId.Equals(privateSchedule.DoctorId));
                var view = new PublicScheduleViewModel().Presenter(privateSchedule, doctorInfo);
                views.Add(view);
            }
            return views.OrderBy(x => x.TargetDateTime).ThenBy(x => x.DoctorName);
        }

        public IEnumerable<PublicScheduleViewModel> GetDoctorPublicScheduleService(string doctorId)
        {
            var privateScheduleList = _publicScheduleRepository.GetDoctorPublicSchedule(doctorId);
            var doctorInfo = _publicScheduleRepository.GetTargetDoctorList(new[] { doctorId }).First();

            var views = new List<PublicScheduleViewModel>();
            foreach (var privateSchedule in privateScheduleList)
            {
                var view = new PublicScheduleViewModel().Presenter(privateSchedule, doctorInfo);
                views.Add(view);
            }
            return views.OrderBy(x => x.TargetDateTime);
        }

        public IEnumerable<PublicScheduleViewModel> GetDoctorPublicScheduleForTheSpecifiedWeekService(string doctorId, DateTime startDate)
        {
            var privateScheduleList = _publicScheduleRepository.GetDoctorPublicScheduleForTheSpecifiedWeek(doctorId, startDate);
            var doctorInfo = _publicScheduleRepository.GetTargetDoctorList(new[] { doctorId }).First();

            var views = new List<PublicScheduleViewModel>();
            foreach (var privateSchedule in privateScheduleList)
            {
                var view = new PublicScheduleViewModel().Presenter(privateSchedule, doctorInfo);
                views.Add(view);
            }
            return views.OrderBy(x => x.TargetDateTime);
        }
    }
}

