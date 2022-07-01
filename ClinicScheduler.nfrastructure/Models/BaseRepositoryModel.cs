using System;
namespace ClinicScheduler.Infrastructure.Models
{
    public class BaseRepositoryModel
    {
        /// <summary>
        /// 作成日時
        /// </summary>
        public DateTime CreateDateTime { get; set; }
        /// <summary>
        /// 最終更新日時
        /// </summary>
        public DateTime LastUpdateTime { get; set; }
        /// <summary>
        /// 最終更新者ID
        /// </summary>
        public string LastUpdateId { get; set; }

        public BaseRepositoryModel()
        {
        }
    }
}

