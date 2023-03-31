using AutoMapper;
using DiplomaProject.EntityModels;
using DiplomaProject.EntityModels.Enums;
using DiplomaProject.Models.Contracts;
using DiplomaProject.Models.DTO;
using DiplomaProject.Models.RequestModels;
using DiplomaProject.Models.ResponseModels;
using DiplomaProject.Repository;

namespace DiplomaProject.Services
{
    public interface IGoalService
    {

        Task<GoalResponseModel> CreateGoalAsync(CreateGoalRequestModel requestModel, CancellationToken cancellationToken);
        Task DeleteGoalAsync(long goalId, CancellationToken cancellationToken);
        Task<GoalResponseModel> GetGoalAsync(long userId, CancellationToken cancellationToken);
    }

    public class GoalService : IGoalService
    {
        private readonly IHealthParametrRepository healthParametrRepository;
        private readonly IGoalRepository goalRepository;
        private readonly ICalorieService calorieService;
        private readonly IMapper mapper;

        public GoalService(IHealthParametrRepository healthParametrRepository, IGoalRepository goalRepository, IMapper mapper, ICalorieService calorieService)
        {
            this.healthParametrRepository = healthParametrRepository;
            this.goalRepository = goalRepository;
            this.mapper = mapper;
            this.calorieService = calorieService;
        }

        public async Task<GoalResponseModel> GetGoalAsync(long userId, CancellationToken cancellationToken)
        {
            var goalDTO = await goalRepository.GetGoalAsync(userId, cancellationToken);

            return mapper.Map<GoalResponseModel>(goalDTO);
        }

        public async Task<GoalResponseModel> CreateGoalAsync(CreateGoalRequestModel requestModel, CancellationToken cancellationToken)
        {
            var userHealthParametrs = await healthParametrRepository.GetHealthParametrAsync(requestModel.UserId, cancellationToken);

            if(requestModel.TargetWeight <= userHealthParametrs.Weight)
            {
                var calorieInfoModel = calorieService.CalculateDailyCalorieDeficit(userHealthParametrs.Weight, userHealthParametrs.Height, userHealthParametrs.Age, userHealthParametrs.Gender, requestModel.ActivityType);
                var goalDuration = calorieService.CalculateDaysToReachTargetWeight(userHealthParametrs.Weight, requestModel.TargetWeight, calorieInfoModel.DailyCalorieDeficit);

                var goalDescription = GenerateGoalDescription(GoalType.LoseWeight, requestModel.TargetWeight, goalDuration);

                var goalDTO = new GoalDTO()
                {
                    UserId = requestModel.UserId,
                    Description = goalDescription,
                    TargetWeight = requestModel.TargetWeight,
                    DurationInDays = goalDuration,
                    DailyCalorie = calorieInfoModel.DailyCalorie
                };

                goalDTO = await goalRepository.CreateGoalAsync(goalDTO, cancellationToken);

                return mapper.Map<GoalResponseModel>(goalDTO);
            }
            else
            {
                return null;
            }

        }

        public async Task DeleteGoalAsync(long goalId, CancellationToken cancellationToken)
        {
            await goalRepository.DeleteGoalAsync(goalId, cancellationToken);
        }

        private string GenerateGoalDescription(GoalType goalType, long targetWeight, long durationInDays)
        {
            if(goalType == GoalType.LoseWeight)
            {
                return $"Привести вес своего тела в {targetWeight}кг через {durationInDays} дней";
            }
            else
            {
                return $"Привести вес своего тела в {targetWeight}кг через {durationInDays} дней";
            }
        }
    }
}
