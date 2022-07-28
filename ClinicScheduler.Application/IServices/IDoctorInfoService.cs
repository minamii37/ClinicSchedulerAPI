using System;
using ClinicScheduler.Application.ViewModels;

namespace ClinicScheduler.Application.IServices
{
    public interface IDoctorInfoService
    {
        /// <summary>
        /// 医師一覧の取得
        /// </summary>
        /// <returns></returns>
        IEnumerable<DoctorInfoViewModel> GetDoctorListService();

        /// <summary>
        /// 新規医師登録
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        DoctorInfoViewModel CreateNewDoctorInfoService(DoctorInfoViewModel request);
    }
}

