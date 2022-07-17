using System;

namespace ClinicScheduler.Domain.Models.ReservationDomainModel.ValueObjects
{
    public record PatientModel(string PatientId, string PatientName, DateTime RegisteredDate);
}

