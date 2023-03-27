using AutoMapper;
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
    }

    public class HealthParametrService : IHealthParametrService
    {
        private readonly IHealthParametrRepository healthParametrRepository;
        private readonly IMapper mapper;

        public HealthParametrService(IHealthParametrRepository healthParametrRepository, IMapper mapper)
        {
            this.healthParametrRepository = healthParametrRepository;
            this.mapper = mapper;
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
    }
}
