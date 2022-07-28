using System;

namespace ClinicScheduler.Domain.Models.ScheduleDomainModel.ValueObjects
{
    public record DoctorInfoModel(string DoctorId, string DoctorName, DateTime RegisteredDate);
}

