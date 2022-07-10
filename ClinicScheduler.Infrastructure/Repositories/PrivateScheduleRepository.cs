using System;
using System.Net.Http;
using ClinicScheduler.Domain.Models;
using ClinicScheduler.Domain.IRepositories;
using ClinicScheduler.Infrastructure.Models;
using Newtonsoft.Json;
using System.Net;

namespace ClinicScheduler.Infrastructure.Repositories
{
    public class PrivateScheduleRepository : IPrivateScheduleRepository
    {
        private readonly string directoryPath = "../ClinicScheduler.Infrastructure/Data";

        public PrivateScheduleRepository()
        {
        }

        public IEnumerable<ScheduleDomainModel> GetPrivateScheduleForTheSpecifiedDate(DateTime specifiedDate)
        {
            var reservations = GetReservations().Where(x => x.TargetDateTime.Date == specifiedDate.Date);
            if (!reservations.Any())
            {
                return Enumerable.Empty<ScheduleDomainModel>();
            }

            // 予約情報が取得できた場合は予約に含まれるドクター情報を取得する
            IEnumerable<string> doctorIdList = reservations.Select(x => x.DoctorId);
            IEnumerable<DoctorInfoRepositoryModel> doctorInfomations = GetDoctorInfomations(doctorIdList);

            // 予約情報に紐づく患者IDを取得し、対象の患者情報を取得する
            var patientIdList = reservations.Select(x => x.PatientId).Distinct();
            var patientInfomations = GetPatientInfomations(patientIdList!);

            return ConvertModels(reservations, doctorInfomations, patientInfomations);
        }

        public IEnumerable<ScheduleDomainModel> GetDoctorPrivateSchedule(string doctorId)
        {
            // ドクター情報を取得する
            var doctorInfomations = GetDoctorInfomations(new[] { doctorId });

            // ドクターに紐づく予約情報を取得する
            var reservations = GetReservations().Where(x => x.DoctorId.Equals(doctorId));
            if (!reservations.Any())
            {
                return Enumerable.Empty<ScheduleDomainModel>();
            }

            // 予約情報に紐づく患者IDを取得し、対象の患者情報を取得する
            var patientIdList = reservations.Select(x => x.PatientId).Distinct();
            var patientInfomations = GetPatientInfomations(patientIdList!);
            
            return ConvertModels(reservations, doctorInfomations, patientInfomations);
        }

        public IEnumerable<ScheduleDomainModel> GetDoctorPrivateScheduleForTheSpecifiedWeek(string doctorId, DateTime startDate)
        {
            // ドクター情報を取得する
            var doctorInfomations = GetDoctorInfomations(new[] { doctorId });

            // ドクターに紐づく開始日から一週間の予約情報を取得する
            var reservations = GetReservations().Where(
                x => x.DoctorId.Equals(doctorId)
                && x.TargetDateTime >= startDate && x.TargetDateTime < startDate.AddDays(7));
            if (!reservations.Any())
            {
                return Enumerable.Empty<ScheduleDomainModel>();
            }

            // 予約情報に紐づく患者IDを取得し、対象の患者情報を取得する
            var patientIdList = reservations.Select(x => x.PatientId).Distinct();
            var patientInfomations = GetPatientInfomations(patientIdList!);

            return ConvertModels(reservations, doctorInfomations, patientInfomations);
        }

        /// <summary>
        /// 予約テーブルデータの取得
        /// </summary>
        /// <returns></returns>
        private IEnumerable<ReservationRepositoryModel> GetReservations()
        {
            // JSONデータの取得
            StreamReader r = new StreamReader($"{directoryPath}/ReservationTable.json");
            string jsonString = r.ReadToEnd();
            // JSONデータのデシリアライズ
            IEnumerable<ReservationRepositoryModel>? repositoryModels =
                JsonConvert.DeserializeObject<IEnumerable<ReservationRepositoryModel>>(jsonString);

            return repositoryModels ?? Enumerable.Empty<ReservationRepositoryModel>();
        }

        /// <summary>
        /// ドクター情報の取得
        /// </summary>
        /// <param name="doctorIdList">ドクターIDリスト</param>
        /// <returns></returns>
        private IEnumerable<DoctorInfoRepositoryModel> GetDoctorInfomations(IEnumerable<string> doctorIdList)
        {
            // JSONデータの取得
            StreamReader r = new StreamReader($"{directoryPath}/DoctorInfoTable.json");
            string jsonString = r.ReadToEnd();
            // JSONデータのデシリアライズ
            IEnumerable<DoctorInfoRepositoryModel>? repositoryModels =
                JsonConvert.DeserializeObject<IEnumerable<DoctorInfoRepositoryModel>>(jsonString);

            // ドクター情報で絞り込み
            repositoryModels = repositoryModels?.Where(x => doctorIdList.Contains(x.DoctorId));

            if (repositoryModels?.Count() == 0)
            {
                throw new HttpRequestException("対象の医師IDは存在しません", null, HttpStatusCode.NotFound);
            }

            return repositoryModels ?? Enumerable.Empty<DoctorInfoRepositoryModel>();
        }

        /// <summary>
        /// 患者情報の取得
        /// </summary>
        /// <param name="patientIdList">患者IDリスト</param>
        /// <returns>患者情報</returns>
        /// <exception cref="HttpRequestException"></exception>
        public IEnumerable<PatientInfoRepositoryModel> GetPatientInfomations(IEnumerable<string> patientIdList)
        {
            // JSONデータの取得
            StreamReader r = new StreamReader($"{directoryPath}/PatientInfoTable.json");
            string jsonString = r.ReadToEnd();
            // JSONデータのデシリアライズ
            IEnumerable<PatientInfoRepositoryModel>? repositoryModels =
                JsonConvert.DeserializeObject<IEnumerable<PatientInfoRepositoryModel>>(jsonString);

            // ドクター情報で絞り込み
            repositoryModels = repositoryModels?.Where(x => patientIdList.Contains(x.PatientId));

            if (repositoryModels?.Count() == 0)
            {
                throw new HttpRequestException("対象の患者IDは存在しません", null, HttpStatusCode.NotFound);
            }

            return repositoryModels ?? Enumerable.Empty<PatientInfoRepositoryModel>();
        }

        /// <summary>
        /// リポジトリモデル→ドメインモデルの変換
        /// </summary>
        /// <param name="reservations"></param>
        /// <returns></returns>
        private IEnumerable<ScheduleDomainModel> ConvertModels(
            IEnumerable<ReservationRepositoryModel> reservations,
            IEnumerable<DoctorInfoRepositoryModel> doctorInfomations,
            IEnumerable<PatientInfoRepositoryModel> patientInfomations)
        {
            IEnumerable<ScheduleDomainModel> domainModels = Enumerable.Empty<ScheduleDomainModel>();

            foreach(var doctorInfo in doctorInfomations)
            {
                foreach (var reservation in reservations.Where(x => x.DoctorId.Equals(doctorInfo.DoctorId)))
                {
                    var domainModel = new ScheduleDomainModel()
                    {
                        DoctorId = doctorInfo.DoctorId,
                        DoctorName = doctorInfo.DoctorName,
                        TargetDateTime = reservation.TargetDateTime,
                        PatientId = reservation.PatientId,
                        PatientName = patientInfomations.First(x => x.PatientId == reservation.PatientId).PatientName,
                        ReservationDateTime = reservation.ReservationDateTime,
                        ApprovalId = reservation.ApprovalId,
                        ApprovalDateTime = reservation.ApprovalDateTime,
                    };

                    domainModels = domainModels.Append(domainModel);
                }
            }

            return domainModels;
        }
    }
}

