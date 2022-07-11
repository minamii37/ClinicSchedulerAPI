using System;
using ClinicScheduler.Application.ViewModels;
using ClinicScheduler.Domain.IRepositories;
using ClinicScheduler.Domain.Models;

namespace ClinicScheduler.Application.IServices
{
    public class PatientService : IPatientService
    {
        private readonly IPatientInfoRepository _patientRepository;

        public PatientService(IPatientInfoRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public IEnumerable<PatientViewModel> GetPatientListService()
            => ConvertToViewModels(_patientRepository.GetPatientList());

        public PatientViewModel CreateNewPatientInfoService(PatientViewModel requestView)
        {
            var requestModel = new PatientViewModel().Transfer(requestView);
            requestModel = _patientRepository.PostNewPatientInfo(requestModel);
            return new PatientViewModel().Presenter(requestModel);
        }

        /// <summary>
        /// ViewModelへ変換
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        private static IEnumerable<PatientViewModel> ConvertToViewModels(IEnumerable<PatientDomainModel> models)
        {
            var views = new List<PatientViewModel>();
            // 表示順は登録日時順
            models.OrderBy(x => x.CreateDateTime).ToList()
                .ForEach(x => views.Add(new PatientViewModel().Presenter(x)));

            return views;
        }
    }
}

