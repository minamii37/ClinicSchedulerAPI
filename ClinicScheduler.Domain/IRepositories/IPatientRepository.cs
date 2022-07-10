using System;
using ClinicScheduler.Domain.Models;

namespace ClinicScheduler.Domain.IRepositories
{
    public interface IPatientRepository
    {
        /// <summary>
        /// 患者一覧の取得
        /// </summary>
        /// <returns></returns>
        IEnumerable<PatientDomainModel> GetPatientList();
    }
}

