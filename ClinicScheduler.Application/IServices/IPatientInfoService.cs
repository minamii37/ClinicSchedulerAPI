using System;
using ClinicScheduler.Application.ViewModels;

namespace ClinicScheduler.Application.IServices
{
    public interface IPatientInfoService
    {
        /// <summary>
        /// 患者一覧の取得
        /// </summary>
        /// <returns></returns>
        IEnumerable<PatientInfoViewModel> GetPatientListService();

        /// <summary>
        /// 新規患者登録
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        PatientInfoViewModel CreateNewPatientInfoService(PatientInfoViewModel request);
    }
}

