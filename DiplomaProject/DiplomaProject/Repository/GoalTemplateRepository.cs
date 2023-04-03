using DiplomaProject.DatabaseSecret;
using DiplomaProject.EntityModels.Enums;
using Microsoft.EntityFrameworkCore;

namespace DiplomaProject.Repository
{
    public interface IGoalTemplateRepository
    {
        Task<string> GetGoalTemplateAsync(GoalType goalType, CancellationToken cancellationToken);
    }

    public class GoalTemplateRepository : IGoalTemplateRepository
    {
        private readonly AppDbContext dbContext;

        public GoalTemplateRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> GetGoalTemplateAsync(GoalType goalType, CancellationToken cancellationToken)
        {
            Random rand = new Random();
            
            var goalTemplates = await dbContext.GoalTemplates
                .Where(x => x.GoalType == goalType)
                .Select(x => x.Description)
                .ToListAsync(cancellationToken);

            if (goalTemplates != null)
            {
                int index = rand.Next(0, goalTemplates.Count - 1);

                return goalTemplates[index];
            }

            return "Привести вес своего тела в {targetWeight} кг через {durationInDays} дней";
        }
    }
}
