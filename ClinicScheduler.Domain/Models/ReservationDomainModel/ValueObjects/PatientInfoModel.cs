using System;

namespace ClinicScheduler.Domain.Models.ReservationDomainModel.ValueObjects
{
    public record PatientInfoModel(string PatientId, string PatientName, DateTime RegisteredDate);
}

