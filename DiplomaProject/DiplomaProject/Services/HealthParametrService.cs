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

        public HealthParametrService(IHealthParametrRepository healthParametrRepository, IMapper mapper, IUserBMIRepository userBMIRepository)
        {
            this.healthParametrRepository = healthParametrRepository;
            this.mapper = mapper;
            this.userBMIRepository = userBMIRepository;
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

            var bmi = CalculateBMI(userHealthParametrs.Weight, userHealthParametrs.Height, userHealthParametrs.Age, userHealthParametrs.Gender);

            var bmiHistoryDTO = await userBMIRepository.AddBMIAsync(bmi, userId, cancellationToken);

            var response = mapper.Map<BMIResponseModel>(bmiHistoryDTO);

            response.Status = GetBMIStatus(bmi);

            return response;
        }

        private double CalculateBMI(long weight, long height, int age, Gender gender)
        {
            double bmi = (weight / Math.Pow((double)height / 100, 2));

            if (gender == Gender.Male)
            {
                bmi += 0.5;

                if (age > 50)
                {
                    bmi += 0.7;
                }
            }
            else if (gender == Gender.Female)
            {
                bmi -= 0.5;

                if (age > 50)
                {
                    bmi += 0.7;
                }
            }

            return bmi;
        }

        private BMIStatus GetBMIStatus(double bmi)
        {
            if (bmi < 18.5)
            {
                return BMIStatus.Underweight;
            }
            else if (bmi < 25)
            {
                return BMIStatus.NormalWeight;
            }
            else if (bmi < 30)
            {
                return BMIStatus.Overweight;
            }
            else
            {
                return BMIStatus.Obese;
            }
        }
    }
}