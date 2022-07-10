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

    [HttpGet("patientId")]
    public IEnumerable<ReservationViewModel> GetOwnReservation(string patientId)
    {
        return _reservateService.GetOwnReservationsService(patientId);
    }

    [HttpPost]
    public ReservationViewModel PostReservation([FromBody]ReservationViewModel views)
    {
        return _reservateService.PostReservationService(views);
    }
}

