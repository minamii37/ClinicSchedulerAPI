using System;

namespace ClinicScheduler.Domain.Models.ReservationDomainModel.ValueObjects
{
    public record DoctorModel(string DoctorId, string DoctorName, DateTime RegisteredDate);
}

