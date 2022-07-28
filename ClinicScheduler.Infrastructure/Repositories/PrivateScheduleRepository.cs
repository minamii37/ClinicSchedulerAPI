using System;
using ClinicScheduler.Domain.IRepositories;
using ClinicScheduler.Infrastructure.Models;
using Newtonsoft.Json;
using ClinicScheduler.Domain.Models.ScheduleDomainModel;
using ClinicScheduler.Infrastructure.DBAccess;
using ClinicScheduler.Domain.Models.ScheduleDomainModel.ValueObjects;

namespace ClinicScheduler.Infrastructure.Repositories
{
    public class PrivateScheduleRepository : IPrivateScheduleRepository
    {
        public PrivateScheduleRepository()
        {
        }

        public IEnumerable<ScheduleDomainModel> GetPrivateScheduleForTheSpecifiedDate(DateTime specifiedDate)
        {
            var reservations = new TrnReservations()
                .GetAllReservations().Where(x => x.TargetDateTime.Date == specifiedDate.Date);
            if (!reservations.Any())
            {
                return Enumerable.Empty<ScheduleDomainModel>();
            }

            return ConvertReservationModels(reservations);
        }

        public IEnumerable<ScheduleDomainModel> GetDoctorPrivateSchedule(string doctorId)
        {
            // ドクターに紐づく予約情報を取得する
            var reservations = new TrnReservations()
                .GetAllReservations().Where(x => x.DoctorId.Equals(doctorId));
            if (!reservations.Any())
            {
                return Enumerable.Empty<ScheduleDomainModel>();
            }

            return ConvertReservationModels(reservations);
        }

        public IEnumerable<ScheduleDomainModel> GetDoctorPrivateScheduleForTheSpecifiedWeek(string doctorId, DateTime startDate)
        {
            // ドクターに紐づく開始日から一週間の予約情報を取得する
            var reservations = new TrnReservations()
                .GetAllReservations().Where(x => x.DoctorId.Equals(doctorId)
                && x.TargetDateTime >= startDate && x.TargetDateTime < startDate.AddDays(7));
            if (!reservations.Any())
            {
                return Enumerable.Empty<ScheduleDomainModel>();
            }

            return ConvertReservationModels(reservations);
        }

        public IEnumerable<DoctorInfoModel> GetTargetDoctorList(IEnumerable<string> doctorIdList)
        {
            var repositoryModels = new MstDoctorInfomations().GetTargetDoctorInfomationsFromDB(doctorIdList);
            if (!repositoryModels.Any())
            {
                throw new InvalidOperationException("対象の医師IDは存在しません");
            }

            return ConvertDoctorInfoModels(repositoryModels);
        }

        public IEnumerable<PatientInfoModel> GetTargetPatientList(IEnumerable<string> patientIdList)
        {
            var repositoryModels = new MstPatientInfomations().GetAllPatientInfoFromDB();

            // 患者情報で絞り込み
            repositoryModels = repositoryModels.Where(x => patientIdList.Contains(x.PatientId));
            if (!repositoryModels.Any())
            {
                throw new InvalidOperationException("対象の患者IDは存在しません");
            }

            return ConvertPatientInfoModels(repositoryModels);
        }

        /// <summary>
        /// リポジトリモデル→ドメインモデルの変換
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns></returns>
        private IEnumerable<ScheduleDomainModel> ConvertReservationModels(IEnumerable<ReservationRepositoryModel> reservations)
        {
            var domainModels = new List<ScheduleDomainModel>();
            foreach (var reservation in reservations)
            {
                var domainModel = new ScheduleDomainModel()
                {
                    DoctorId = reservation.DoctorId,
                    TargetDateTime = reservation.TargetDateTime,
                    PatientId = reservation.PatientId,
                    ReservationDateTime = reservation.ReservationDateTime,
                    ApprovalId = reservation.ApprovalId,
                    ApprovalDateTime = reservation.ApprovalDateTime,
                };
                domainModels.Add(domainModel);
            }

            return domainModels;
        }

        /// <summary>
        /// 医師情報リポジトリモデル→ValueObjectモデル
        /// </summary>
        /// <param name="doctorInfos"></param>
        /// <returns></returns>
        private IEnumerable<DoctorInfoModel> ConvertDoctorInfoModels(IEnumerable<DoctorInfoRepositoryModel> doctorInfos)
        {
            var valueObjects = new List<DoctorInfoModel>();
            foreach (var doctorInfo in doctorInfos)
            {
                var valueObject = new DoctorInfoModel(doctorInfo.DoctorId, doctorInfo.DoctorName, doctorInfo.CreateDate);
                valueObjects.Add(valueObject);
            }

            return valueObjects;
        }

        /// <summary>
        /// 患者情報リポジトリモデル→ValueObjectモデル
        /// </summary>
        /// <param name="patientInfos"></param>
        /// <returns></returns>
        private IEnumerable<PatientInfoModel> ConvertPatientInfoModels(IEnumerable<PatientInfoRepositoryModel> patientInfos)
        {
            var valueObjects = new List<PatientInfoModel>();
            foreach (var patientInfo in patientInfos)
            {
                var valueObject = new PatientInfoModel(patientInfo.PatientId, patientInfo.PatientName, patientInfo.CreateDate);
                valueObjects.Add(valueObject);
            }

            return valueObjects;
        }
    }
}

