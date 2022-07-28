using System;
using System.Net.Http;
using ClinicScheduler.Domain.IRepositories;
using ClinicScheduler.Infrastructure.Models;
using Newtonsoft.Json;
using System.Net;
using ClinicScheduler.Domain.Models.ScheduleDomainModel;
using ClinicScheduler.Infrastructure.DBAccess;
using ClinicScheduler.Domain.Models.ScheduleDomainModel.ValueObjects;

namespace ClinicScheduler.Infrastructure.Repositories
{
    public class PublicScheduleRepository : IPublicScheduleRepository
    {
        public PublicScheduleRepository()
        {
        }

        public IEnumerable<ScheduleDomainModel> GetPublicSchedule()
        {
            var reservations = new TrnReservations().GetAllReservations();
            if (!reservations.Any())
            {
                return Enumerable.Empty<ScheduleDomainModel>();
            }

            return ConvertScheduleModels(reservations);
        }

        public IEnumerable<ScheduleDomainModel> GetDoctorPublicSchedule(string doctorId)
        {
            // ドクターに紐づく予約情報を取得する
            var reservations = new TrnReservations()
                .GetAllReservations().Where(x => x.DoctorId.Equals(doctorId));
            if (!reservations.Any())
            {
                return Enumerable.Empty<ScheduleDomainModel>();
            }

            return ConvertScheduleModels(reservations);
        }

        public IEnumerable<ScheduleDomainModel> GetDoctorPublicScheduleForTheSpecifiedWeek(string doctorId, DateTime startDate)
        {
            // ドクターに紐づく開始日から一週間の予約情報を取得する
            var reservations = new TrnReservations()
                .GetAllReservations().Where(x => x.DoctorId.Equals(doctorId)
                    && x.TargetDateTime >= startDate && x.TargetDateTime < startDate.AddDays(7));
            if (!reservations.Any())
            {
                return Enumerable.Empty<ScheduleDomainModel>();
            }

            return ConvertScheduleModels(reservations);
        }

        public IEnumerable<DoctorInfoModel> GetTargetDoctorList(IEnumerable<string> doctorIdList)
        {
            var repositoryModels = new MstDoctorInfomations().GetTargetDoctorInfomationsFromDB(doctorIdList);
            if (!repositoryModels.Any())
            {
                throw new HttpRequestException("対象の医師IDは存在しません", null, HttpStatusCode.NotFound);
            }

            return ConvertDoctorInfoModels(repositoryModels);
        }

        /// <summary>
        /// リポジトリモデル→ドメインモデルの変換
        /// </summary>
        /// <param name="reservations"></param>
        /// <returns></returns>
        private IEnumerable<ScheduleDomainModel> ConvertScheduleModels(IEnumerable<ReservationRepositoryModel> reservations)
        {
            var domainModels = new List<ScheduleDomainModel>();
            foreach (var reservation in reservations)
            {
                var domainModel = new ScheduleDomainModel()
                {
                    DoctorId = reservation.DoctorId,
                    TargetDateTime = reservation.TargetDateTime,
                    ReservationDateTime = reservation.ReservationDateTime,
                };

                domainModels.Add(domainModel);
            }

            return domainModels;
        }

        /// <summary>
        /// リポジトリモデル→ValueObjectモデルの変換
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
    }
}

