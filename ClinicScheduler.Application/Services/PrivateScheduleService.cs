using System;
using ClinicScheduler.Application.ViewModels;
using ClinicScheduler.Domain.IRepositories;
using ClinicScheduler.Domain.Models;

namespace ClinicScheduler.Application.IServices
{
    public class PrivateScheduleService : IPrivateScheduleService
    {
        private readonly IPrivateScheduleRepository _privateScheduleRepository;

        public PrivateScheduleService(IPrivateScheduleRepository privateScheduleRepository)
        {
            _privateScheduleRepository = privateScheduleRepository;
        }

        public IEnumerable<ScheduleViewModel> GetPrivateScheduleForTheSpecifiedDateService(DateTime specifiedDate)
            => ConvertToViewModels(_privateScheduleRepository.GetPrivateScheduleForTheSpecifiedDate(specifiedDate));

        public IEnumerable<ScheduleViewModel>GetDoctorPrivateScheduleService(string doctorId)
            => ConvertToViewModels(_privateScheduleRepository.GetDoctorPrivateSchedule(doctorId));

        public IEnumerable<ScheduleViewModel> GetDoctorPrivateScheduleForTheSpecifiedWeekService(string doctorId, DateTime startDate)
            => ConvertToViewModels(_privateScheduleRepository.GetDoctorPrivateScheduleForTheSpecifiedWeek(doctorId, startDate));

        /// <summary>
        /// ViewModelへ変換
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        private static IEnumerable<ScheduleViewModel> ConvertToViewModels(IEnumerable<ScheduleDomainModel> models)
        {
            var views = new List<ScheduleViewModel>();
            // 表示順は日付、ドクター名順
            models.OrderBy(x => x.TargetDateTime).ThenBy(x => x.DoctorName).ToList()
                .ForEach(x => views.Add(new ScheduleViewModel().PrivatePresenter(x)));

            return views;
        }
    }
}

