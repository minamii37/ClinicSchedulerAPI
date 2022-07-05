using System;
using ClinicScheduler.Application.ViewModels;
using ClinicScheduler.Domain.IRepositories;
using ClinicScheduler.Domain.Models;

namespace ClinicScheduler.Application.IServices
{
    public class PublicScheduleService : IPublicScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;

        public PublicScheduleService(IScheduleRepository scheduleRepositpry)
        {
            _scheduleRepository = scheduleRepositpry;
        }

        public IEnumerable<ScheduleViewModel> GetPublicScheduleService()
            => ConvertToViewModels(_scheduleRepository.GetPublicSchedule());

        public IEnumerable<ScheduleViewModel> GetPublicScheduleService(string doctorId)
            => ConvertToViewModels(_scheduleRepository.GetPublicScheduleByDoctorId(doctorId));

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
                .ForEach(x => views.Add(new ScheduleViewModel().PublicPresenter(x)));

            return views;
        }
    }
}

