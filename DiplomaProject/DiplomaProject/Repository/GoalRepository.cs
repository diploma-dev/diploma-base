using AutoMapper;
using DiplomaProject.DatabaseSecret;
using DiplomaProject.EntityModels;
using DiplomaProject.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace DiplomaProject.Repository
{
    public interface IGoalRepository
    {
        Task<GoalDTO> CreateGoalAsync(GoalDTO goalDTO, CancellationToken cancellationToken);
        Task DeleteGoalAsync(long goalId, CancellationToken cancellationToken);
        Task<GoalDTO> GetGoalAsync(long userId, CancellationToken cancellationToken);
    }

    public class GoalRepository : IGoalRepository
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;

        public GoalRepository(AppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<GoalDTO> GetGoalAsync(long userId, CancellationToken cancellationToken)
        {
            var currentGoal = await dbContext.Goals.Where(x => x.UserId == userId).FirstOrDefaultAsync(cancellationToken);

            if(currentGoal != null)
            {
                return mapper.Map<GoalDTO>(currentGoal);
            }

            return null; //need to implement not found exception
        }

        public async Task<GoalDTO> CreateGoalAsync(GoalDTO goalDTO, CancellationToken cancellationToken)
        {
            var currentGoal = await dbContext.Goals.Where(x => x.UserId == goalDTO.UserId).FirstOrDefaultAsync(cancellationToken);

            if(currentGoal != null)
            {
                dbContext.Goals.Remove(currentGoal);

                await dbContext.SaveChangesAsync(cancellationToken);
            }

            var goal = new GoalEntity()
            {
                Description = goalDTO.Description,
                TargetWeight = goalDTO.TargetWeight,
                DurationInDays = goalDTO.DurationInDays,
                DailyCalorie = goalDTO.DailyCalorie,
                UserId = goalDTO.UserId,
            };

            await dbContext.Goals.AddAsync(goal, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return mapper.Map<GoalDTO>(goal);
        }

        public async Task DeleteGoalAsync(long goalId, CancellationToken cancellationToken)
        {
            var goal = await dbContext.Goals.Where(x => x.Id == goalId).FirstOrDefaultAsync(cancellationToken);

            if(goal != null)
            {
                dbContext.Goals.Remove(goal);

                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
