﻿using System;
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

        public IEnumerable<ScheduleViewModel> GetPublicScheduleService()
        => ConvertToViewModels(_publicScheduleRepository.GetPublicSchedule());

        public IEnumerable<ScheduleViewModel>GetDoctorPublicScheduleService(string doctorId)
            => ConvertToViewModels(_publicScheduleRepository.GetDoctorPublicSchedule(doctorId));

        public IEnumerable<ScheduleViewModel> GetDoctorPublicScheduleForTheSpecifiedWeekService(string doctorId, DateTime startDate)
            => ConvertToViewModels(_publicScheduleRepository.GetDoctorPublicScheduleForTheSpecifiedWeek(doctorId, startDate));

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
