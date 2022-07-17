using System;
using ClinicScheduler.Domain.Models.PatientDomainModel;

namespace ClinicScheduler.Domain.IRepositories
{
    public interface IPatientInfoRepository
    {
        /// <summary>
        /// 患者一覧の取得
        /// </summary>
        /// <returns></returns>
        IEnumerable<PatientDomainModel> GetPatientList();

        /// <summary>
        /// 新規患者登録
        /// </summary>
        /// <param name="request">登録情報</param>
        /// <returns>登録内容</returns>
        PatientDomainModel PostNewPatientInfo(PatientDomainModel request);
    }
}

