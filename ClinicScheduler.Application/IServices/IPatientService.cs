using System;
using ClinicScheduler.Application.ViewModels;

namespace ClinicScheduler.Application.IServices
{
    public interface IPatientService
    {
        IEnumerable<PatientViewModel> GetPatientListService();        
    }
}

