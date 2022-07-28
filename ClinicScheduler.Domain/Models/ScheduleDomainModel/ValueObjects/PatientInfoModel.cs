using System;

namespace ClinicScheduler.Domain.Models.ScheduleDomainModel.ValueObjects
{
    public record PatientInfoModel(string PatientId, string PatientName, DateTime RegisteredDate);
}

