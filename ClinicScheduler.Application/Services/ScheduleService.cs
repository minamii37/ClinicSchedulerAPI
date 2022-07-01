using System;
using ClinicScheduler.Application.ViewModels;

namespace ClinicScheduler.Application.IServices
{
    public class Schedule : IScheduleService
    {
        public Schedule()
        {
        }

        public IEnumerable<ScheduleViewModel> GetScheduleService()
        {
            return Enumerable.Empty<ScheduleViewModel>();
        }
    }
}

