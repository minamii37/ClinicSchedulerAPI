using System;
using ClinicScheduler.Domain.Models.ReservationDomainModel;

namespace ClinicScheduler.Domain.IRepositories
{
    public interface IReservateRepository
    {
        /// <summary>
        /// 自分の予約情報の取得
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        IEnumerable<ReservationDomainModel> GetOwnReservations(string patientId);

        /// <summary>
        /// 既存の関連する予約の取得
        /// </summary>
        /// <param name="request">リクエスト</param>
        /// <returns>既存の自予約（存在しない場合はnull）</returns>
        IEnumerable<ReservationDomainModel> GetRelatedExistingReservation(ReservationDomainModel request);

        /// <summary>
        /// 指定日時の予約の取得
        /// </summary>
        /// <param name="specifiedDateTime">対象日時</param>
        /// <returns></returns>
        ReservationDomainModel GetSpecifiedDateReservation(DateTime specifiedDateTime);

        /// <summary>
        /// 予約の実施
        /// </summary>
        /// <returns>予約成否</returns>
        ReservationDomainModel PostReservation(ReservationDomainModel model);
    }
}

