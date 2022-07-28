using System;
using ClinicScheduler.Domain.Models.DoctorInfoDomainModel;

namespace ClinicScheduler.Domain.IRepositories
{
    public interface IDoctorInfoRepository
    {
        /// <summary>
        /// 医師一覧の取得
        /// </summary>
        /// <returns></returns>
        IEnumerable<DoctorInfoDomainModel> GetDoctorList();

        /// <summary>
        /// 新規医師登録
        /// </summary>
        /// <param name="request">登録情報</param>
        /// <returns>登録内容</returns>
        DoctorInfoDomainModel PostNewDoctorInfo(DoctorInfoDomainModel request);
    }
}

