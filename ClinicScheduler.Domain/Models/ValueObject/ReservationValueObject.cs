using System;
namespace ClinicScheduler.Domain.Models.ValueObjects
{
    public record ReservationValueObject(
        string ReservationId, string DoctorId, DateTime TargetDateTime, string PatintId, DateTime ReservationDatetime);
}

