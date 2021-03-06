using ClinicScheduler.Application.IServices;
using ClinicScheduler.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ClinicSchedulerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PublicScheduleController : ControllerBase
{
    private readonly IPublicScheduleService _publicScheduleService;

    public PublicScheduleController(IPublicScheduleService publicScheduleService)
    {
        _publicScheduleService = publicScheduleService;
    }

    [HttpGet]
    public IEnumerable<PublicScheduleViewModel> GetPublicSchedule()
    {
        return _publicScheduleService.GetPublicScheduleService().ToList();
    }

    [HttpGet("doctorId")]
    public IEnumerable<PublicScheduleViewModel> GetDoctorPublicSchedule(string doctorId)
    {
        return _publicScheduleService.GetDoctorPublicScheduleService(doctorId).ToList();
    }

    [HttpGet("doctorId/startDate")]
    public IEnumerable<PublicScheduleViewModel> GetDoctorPublicScheduleForTheSpecifiedWeek(string doctorId, DateTime startDate)
    {
        return _publicScheduleService.GetDoctorPublicScheduleForTheSpecifiedWeekService(doctorId, startDate).ToList();
    }
}

