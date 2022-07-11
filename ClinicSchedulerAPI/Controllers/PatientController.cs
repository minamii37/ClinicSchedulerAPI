using ClinicScheduler.Application.IServices;
using ClinicScheduler.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ClinicSchedulerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet()]
    public IEnumerable<PatientViewModel> GetPatientList()
    {
        return _patientService.GetPatientListService().ToList();
    }

    [HttpPost]
    public PatientViewModel CreateNewPatientInfoService([FromBody]PatientViewModel request)
    {
        return _patientService.CreateNewPatientInfoService(request);
    }
}

