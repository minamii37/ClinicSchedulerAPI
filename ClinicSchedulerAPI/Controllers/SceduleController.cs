using ClinicScheduler.Application.IServices;
using ClinicScheduler.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ClinicSchedulerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScheduleController : ControllerBase
{
    private readonly IScheduleService _test;

    public ScheduleController(ILogger<WeatherForecastController> logger, IScheduleService test)
    {
        _test = test;
    }

    //[HttpGet("Piyo")]
    [HttpGet(Name = "GetTestPiyo")]
    public IEnumerable<ScheduleViewModel> GetTestPiyo()
    {
        return _test.GetScheduleService().ToList();
    }
}

