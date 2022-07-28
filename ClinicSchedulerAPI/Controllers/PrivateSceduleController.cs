using ClinicScheduler.Application.IServices;
using ClinicScheduler.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ClinicSchedulerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrivateScheduleController : ControllerBase
{
    private readonly IPrivateScheduleService _privateScheduleService;

    public PrivateScheduleController(IPrivateScheduleService privateScheduleService)
    {
        _privateScheduleService = privateScheduleService;
    }

    [HttpGet("specifiedDate")]
    public IEnumerable<PrivateScheduleViewModel> GetPrivateSchedule(DateTime specifiedDate)
    {
        return _privateScheduleService.GetPrivateScheduleForTheSpecifiedDateService(specifiedDate).ToList();
    }

    [HttpGet("doctorId")]
    public IEnumerable<PrivateScheduleViewModel> GetDoctorPrivateSchedule(string doctorId)
    {
        return _privateScheduleService.GetDoctorPrivateScheduleService(doctorId).ToList();
    }

    [HttpGet("doctorId/startDate")]
    public IEnumerable<PrivateScheduleViewModel> GetDoctorPrivateScheduleForTheSpecifiedWeek(string doctorId, DateTime startDate)
    {
        return _privateScheduleService.GetDoctorPrivateScheduleForTheSpecifiedWeekService(doctorId, startDate).ToList();
    }
}

