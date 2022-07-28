using ClinicScheduler.Application.IServices;
using ClinicScheduler.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ClinicSchedulerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly IPatientInfoService _patientService;

    public PatientController(IPatientInfoService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet()]
    public IEnumerable<PatientInfoViewModel> GetPatientList()
    {
        return _patientService.GetPatientListService().ToList();
    }

    [HttpPost]
    public PatientInfoViewModel CreateNewPatientInfoService([FromBody]PatientInfoViewModel request)
    {
        return _patientService.CreateNewPatientInfoService(request);
    }
}

