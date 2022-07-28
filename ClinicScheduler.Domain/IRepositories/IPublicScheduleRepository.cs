using System;
using ClinicScheduler.Domain.Models.ScheduleDomainModel;
using ClinicScheduler.Domain.Models.ScheduleDomainModel.ValueObjects;

namespace ClinicScheduler.Domain.IRepositories
{
    public interface IPublicScheduleRepository
    {
        /// <summary>
        /// 全スケジュールの取得
        /// </summary>
        /// <returns>全スケジュール</returns>
        IEnumerable<ScheduleDomainModel> GetPublicSchedule();

        /// <summary>
        /// ドクター別スケジュールの取得
        /// </summary>
        /// <param name="doctorId">ドクターID</param>
        /// <returns>ドクター別スケジュール</returns>
        IEnumerable<ScheduleDomainModel> GetDoctorPublicSchedule(string doctorId);

        /// <summary>
        /// ドクター別指定週スケジュールの取得
        /// </summary>
        /// <param name="doctorId">ドクターID</param>
        /// <param name="startDate">スケジュール開始日</param>
        /// <returns>ドクター別スケジュール</returns>
        IEnumerable<ScheduleDomainModel> GetDoctorPublicScheduleForTheSpecifiedWeek(string doctorId, DateTime startDate);

        /// <summary>
        /// 対象の医師一覧の取得
        /// </summary>
        /// <returns></returns>
        IEnumerable<DoctorInfoModel> GetTargetDoctorList(IEnumerable<string> doctorIds);
    }
}

