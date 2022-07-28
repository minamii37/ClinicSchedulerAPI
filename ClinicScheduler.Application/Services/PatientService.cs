using System;
using ClinicScheduler.Application.ViewModels;
using ClinicScheduler.Domain.IRepositories;
using ClinicScheduler.Domain.Models.PatientInfoDomainModel;

namespace ClinicScheduler.Application.IServices
{
    public class PatientInfoService : IPatientInfoService
    {
        private readonly IPatientInfoRepository _patientInfoRepository;

        public PatientInfoService(IPatientInfoRepository patientInfoRepository)
        {
            _patientInfoRepository = patientInfoRepository;
        }

        public IEnumerable<PatientInfoViewModel> GetPatientListService()
            => ConvertToViewModels(_patientInfoRepository.GetPatientList());

        public PatientInfoViewModel CreateNewPatientInfoService(PatientInfoViewModel requestView)
        {
            var requestModel = new PatientInfoViewModel().Transfer(requestView);
            requestModel = _patientInfoRepository.PostNewPatientInfo(requestModel);
            return new PatientInfoViewModel().Presenter(requestModel);
        }

        /// <summary>
        /// ViewModelへ変換
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        private static IEnumerable<PatientInfoViewModel> ConvertToViewModels(IEnumerable<PatientInfoDomainModel> models)
        {
            var views = new List<PatientInfoViewModel>();
            // 表示順は登録日時順
            models.OrderBy(x => x.CreateDateTime).ToList()
                .ForEach(x => views.Add(new PatientInfoViewModel().Presenter(x)));

            return views;
        }
    }
}

