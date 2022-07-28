using System;
using ClinicScheduler.Domain.Models.ScheduleDomainModel;
using ClinicScheduler.Domain.Models.ScheduleDomainModel.ValueObjects;

namespace ClinicScheduler.Domain.IRepositories
{
    public interface IPrivateScheduleRepository
    {
        /// <summary>
        /// 指定日の非公開スケジュールの取得
        /// </summary>
        /// <returns>指定日スケジュール</returns>
        IEnumerable<ScheduleDomainModel> GetPrivateScheduleForTheSpecifiedDate(DateTime specifiedDate);

        /// <summary>
        /// ドクター別非公開スケジュールの取得
        /// </summary>
        /// <param name="doctorId">ドクターID</param>
        /// <returns>ドクター別非公開スケジュール</returns>
        IEnumerable<ScheduleDomainModel> GetDoctorPrivateSchedule(string doctorId);

        /// <summary>
        /// ドクター別の指定週非公開スケジュールの取得
        /// </summary>
        /// <param name="doctorId">ドクターID</param>
        /// <param name="startDate">スケジュール開始日</param>
        /// <returns>ドクター別の指定週非公開スケジュール</returns>
        IEnumerable<ScheduleDomainModel> GetDoctorPrivateScheduleForTheSpecifiedWeek(string doctorId, DateTime startDate);


        /// <summary>
        /// 対象の医師一覧の取得
        /// </summary>
        /// <returns></returns>
        IEnumerable<DoctorInfoModel> GetTargetDoctorList(IEnumerable<string> doctorIds);


        /// <summary>
        /// 対象の患者一覧の取得
        /// </summary>
        /// <returns></returns>
        IEnumerable<PatientInfoModel> GetTargetPatientList(IEnumerable<string> patientIds);
    }
}

