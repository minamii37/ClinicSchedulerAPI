using System;
using ClinicScheduler.Application.ViewModels;
using ClinicScheduler.Domain.IRepositories;

namespace ClinicScheduler.Application.IServices
{
    public class PrivateScheduleService : IPrivateScheduleService
    {
        private readonly IPrivateScheduleRepository _privateScheduleRepository;

        public PrivateScheduleService(IPrivateScheduleRepository privateScheduleRepository)
        {
            _privateScheduleRepository = privateScheduleRepository;
        }

        public IEnumerable<PrivateScheduleViewModel> GetPrivateScheduleForTheSpecifiedDateService(DateTime specifiedDate)
        {
            var privateScheduleList = _privateScheduleRepository.GetPrivateScheduleForTheSpecifiedDate(specifiedDate);
            var doctorInfos = _privateScheduleRepository.GetTargetDoctorList(privateScheduleList.Select(x => x.DoctorId));
            var patientInfos = _privateScheduleRepository.GetTargetPatientList(privateScheduleList.Select(x => x.PatientId));

            var views = new List<PrivateScheduleViewModel>();
            foreach (var privateSchedule in privateScheduleList)
            {
                var doctorInfo = doctorInfos.First(x => x.DoctorId.Equals(privateSchedule.DoctorId));
                var patientInfo = patientInfos.First(x => x.PatientId.Equals(privateSchedule.PatientId));
                var view = new PrivateScheduleViewModel().Presenter(privateSchedule, doctorInfo, patientInfo);
                views.Add(view);
            }
            return views.OrderBy(x => x.TargetDateTime).ThenBy(x => x.DoctorName).ThenBy(x => x.PatientName);
        }

        public IEnumerable<PrivateScheduleViewModel> GetDoctorPrivateScheduleService(string doctorId)
        {
            var privateScheduleList = _privateScheduleRepository.GetDoctorPrivateSchedule(doctorId);
            var doctorInfo = _privateScheduleRepository.GetTargetDoctorList(new[] { doctorId }).First();
            var patientInfos = _privateScheduleRepository.GetTargetPatientList(privateScheduleList.Select(x => x.PatientId));

            var views = new List<PrivateScheduleViewModel>();
            foreach (var privateSchedule in privateScheduleList)
            {
                var patientInfo = patientInfos.First(x => x.PatientId.Equals(privateSchedule.PatientId));
                var view = new PrivateScheduleViewModel().Presenter(privateSchedule, doctorInfo, patientInfo);
                views.Add(view);
            }
            return views.OrderBy(x => x.TargetDateTime).ThenBy(x => x.PatientName);
        }

        public IEnumerable<PrivateScheduleViewModel> GetDoctorPrivateScheduleForTheSpecifiedWeekService(string doctorId, DateTime startDate)
        {
            var privateScheduleList = _privateScheduleRepository.GetDoctorPrivateScheduleForTheSpecifiedWeek(doctorId, startDate);
            var doctorInfo = _privateScheduleRepository.GetTargetDoctorList(new[] { doctorId }).First();
            var patientInfos = _privateScheduleRepository.GetTargetPatientList(privateScheduleList.Select(x => x.PatientId));

            var views = new List<PrivateScheduleViewModel>();
            foreach (var privateSchedule in privateScheduleList)
            {
                var patientInfo = patientInfos.First(x => x.PatientId.Equals(privateSchedule.PatientId));
                var view = new PrivateScheduleViewModel().Presenter(privateSchedule, doctorInfo, patientInfo);
                views.Add(view);
            }
            return views.OrderBy(x => x.TargetDateTime).ThenBy(x => x.PatientName);
        }
    }
}

