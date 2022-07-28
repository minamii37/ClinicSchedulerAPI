using System;

namespace ClinicScheduler.Domain.Models.ReservationDomainModel.ValueObjects
{
    public record DoctorInfoModel(string DoctorId, string DoctorName, DateTime RegisteredDate);
}

