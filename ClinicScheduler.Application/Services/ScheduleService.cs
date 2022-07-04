﻿using System;
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

        public IEnumerable<PublicScheduleViewModel> GetPublicScheduleService()
            => ConvertToViewModels(_scheduleRepository.GetPublicSchedule());

        public IEnumerable<PublicScheduleViewModel> GetPublicScheduleService(string doctorId)
            => ConvertToViewModels(_scheduleRepository.GetPublicScheduleByDoctorId(doctorId));

        /// <summary>
        /// ViewModelへ変換
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        private static IEnumerable<PublicScheduleViewModel> ConvertToViewModels(IEnumerable<ScheduleDomainModel> models)
        {
            var views = new List<PublicScheduleViewModel>();
            models.ToList().ForEach(x => views.Add(new PublicScheduleViewModel().Converter(x)));

            return views;
        }
    }
}

