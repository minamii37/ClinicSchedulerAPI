using ClinicScheduler.Application.IServices;
using ClinicScheduler.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ClinicSchedulerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController : ControllerBase
{
    private readonly IReservateService _reservateService;

    public ReservationController(IReservateService reservateService)
    {
        _reservateService = reservateService;
    }

    [HttpPost]
    public PublicScheduleViewModel PostReservation([FromBody]PublicScheduleViewModel views)
    {
        return _reservateService.PostReservationService(views);
    }
}

