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
    public IEnumerable<PublicScheduleViewModel> GetPublicSchedule()
    {
        return _publicScheduleService.GetPublicScheduleService().ToList();
    }

    [HttpGet("doctorId")]
    public IEnumerable<PublicScheduleViewModel> GetPublicScheduleByDoctorId(string doctorId)
    {
        return _publicScheduleService.GetPublicScheduleService(doctorId).ToList();
    }
}

