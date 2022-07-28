using System;
using ClinicScheduler.Application.ViewModels;
using ClinicScheduler.Domain.IRepositories;
using ClinicScheduler.Domain.Models.DoctorInfoDomainModel;

namespace ClinicScheduler.Application.IServices
{
    public class DoctorInfoService : IDoctorInfoService
    {
        private readonly IDoctorInfoRepository _doctorInfoRepository;

        public DoctorInfoService(IDoctorInfoRepository doctorInfoRepository)
        {
            _doctorInfoRepository = doctorInfoRepository;
        }

        public IEnumerable<DoctorInfoViewModel> GetDoctorListService()
            => ConvertToViewModels(_doctorInfoRepository.GetDoctorList());

        public DoctorInfoViewModel CreateNewDoctorInfoService(DoctorInfoViewModel requestView)
        {
            var requestModel = new DoctorInfoViewModel().Transfer(requestView);
            requestModel = _doctorInfoRepository.PostNewDoctorInfo(requestModel);
            return new DoctorInfoViewModel().Presenter(requestModel);
        }

        /// <summary>
        /// ViewModelへ変換
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        private static IEnumerable<DoctorInfoViewModel> ConvertToViewModels(IEnumerable<DoctorInfoDomainModel> models)
        {
            var views = new List<DoctorInfoViewModel>();
            // 表示順は登録日時順
            models.OrderBy(x => x.CreateDateTime).ToList()
                .ForEach(x => views.Add(new DoctorInfoViewModel().Presenter(x)));

            return views;
        }
    }
}

