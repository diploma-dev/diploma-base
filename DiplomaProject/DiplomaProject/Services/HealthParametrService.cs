using AutoMapper;
using DiplomaProject.EntityModels.Enums;
using DiplomaProject.Models.DTO;
using DiplomaProject.Models.RequestModels;
using DiplomaProject.Models.ResponseModels;
using DiplomaProject.Repository;

namespace DiplomaProject.Services
{
    public interface IHealthParametrService
    {
        Task<HealthParametrsResponseModel> GetUserHealthDataAsync(long userId, CancellationToken cancellationToken);
        Task<HealthParametrsResponseModel> UpdateUserHealthDataAsync(UpdateHealthParametrRequestModel requestModel, CancellationToken cancellationToken);
        Task<HealthParametrsResponseModel> UploadUserHealthDataAsync(UploadHealthParametrRequestModel requestModel, CancellationToken cancellationToken);
        Task<BMIResponseModel> GetUserBMIAsync(long userId, CancellationToken cancellationToken);
    }

    public class HealthParametrService : IHealthParametrService
    {
        private readonly IHealthParametrRepository healthParametrRepository;
        private readonly IUserBMIRepository userBMIRepository;
        private readonly IMapper mapper;
        private readonly IBMICalculationService bMICalculationService;

        public HealthParametrService(IHealthParametrRepository healthParametrRepository, IMapper mapper, IUserBMIRepository userBMIRepository, IBMICalculationService bMICalculationService)
        {
            this.healthParametrRepository = healthParametrRepository;
            this.mapper = mapper;
            this.userBMIRepository = userBMIRepository;
            this.bMICalculationService = bMICalculationService;
        }

        public async Task<HealthParametrsResponseModel> GetUserHealthDataAsync(long userId, CancellationToken cancellationToken)
        {
            var userHealthParametrs = await healthParametrRepository.GetHealthParametrAsync(userId, cancellationToken);

            return mapper.Map<HealthParametrsResponseModel>(userHealthParametrs);
        }

        public async Task<HealthParametrsResponseModel> UpdateUserHealthDataAsync(UpdateHealthParametrRequestModel requestModel, CancellationToken cancellationToken)
        {
            var healthParametrDTO = mapper.Map<HealthParametrDTO>(requestModel);

            var userHealthParametrs = await healthParametrRepository.UpdateHealthParametrAsync(healthParametrDTO, cancellationToken);

            return mapper.Map<HealthParametrsResponseModel>(userHealthParametrs);
        }

        public async Task<HealthParametrsResponseModel> UploadUserHealthDataAsync(UploadHealthParametrRequestModel requestModel, CancellationToken cancellationToken)
        {
            var healthParametrDTO = mapper.Map<HealthParametrDTO>(requestModel);

            var userHealthParametrs = await healthParametrRepository.UploadHealthParametrAsync(healthParametrDTO, cancellationToken);

            return mapper.Map<HealthParametrsResponseModel>(userHealthParametrs);
        }

        public async Task<BMIResponseModel> GetUserBMIAsync(long userId, CancellationToken cancellationToken)
        {
            var userHealthParametrs = await healthParametrRepository.GetHealthParametrAsync(userId, cancellationToken);

            if(userHealthParametrs != null)
            {
                var bmi = bMICalculationService.CalculateBMI(userHealthParametrs.Weight, userHealthParametrs.Height, userHealthParametrs.Age, userHealthParametrs.Gender);

                var bmiHistoryDTO = await userBMIRepository.AddBMIAsync(bmi, userId, cancellationToken);

                var response = mapper.Map<BMIResponseModel>(bmiHistoryDTO);

                response.Status = bMICalculationService.GetBMIStatus(bmi);

                return response;
            }

            return null;
        }
    }
}