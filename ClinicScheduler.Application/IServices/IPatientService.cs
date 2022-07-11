using System;
using ClinicScheduler.Application.ViewModels;

namespace ClinicScheduler.Application.IServices
{
    public interface IPatientService
    {
        /// <summary>
        /// 患者一覧の取得
        /// </summary>
        /// <returns></returns>
        IEnumerable<PatientViewModel> GetPatientListService();

        /// <summary>
        /// 新規患者登録
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        PatientViewModel CreateNewPatientInfoService(PatientViewModel request);
    }
}

