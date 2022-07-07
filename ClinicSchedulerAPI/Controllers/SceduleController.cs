using ClinicScheduler.Application.IServices;
using ClinicScheduler.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ClinicSchedulerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScheduleController : ControllerBase
{
    private readonly IPublicScheduleService _publicScheduleService;

    public ScheduleController(IPublicScheduleService publicScheduleService)
    {
        _publicScheduleService = publicScheduleService;
    }

    [HttpGet]
    public IEnumerable<ScheduleViewModel> GetPublicSchedule()
    {
        return _publicScheduleService.GetPublicScheduleService().ToList();
    }

    [HttpGet("doctorId")]
    public IEnumerable<ScheduleViewModel> GetDoctorPublicSchedule(string doctorId)
    {
        return _publicScheduleService.GetDoctorPublicScheduleService(doctorId).ToList();
    }

    [HttpGet("doctorId/startDate")]
    public IEnumerable<ScheduleViewModel> GetDoctorPublicScheduleForTheSpecifiedWeek(string doctorId, DateTime startDate)
    {
        return _publicScheduleService.GetDoctorPublicScheduleForTheSpecifiedWeekService(doctorId, startDate).ToList();
    }
}

