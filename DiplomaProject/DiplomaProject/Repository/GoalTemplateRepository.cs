using DiplomaProject.DatabaseSecret;
using Microsoft.EntityFrameworkCore;

namespace DiplomaProject.Repository
{
    public interface IGoalTemplateRepository
    {
        Task<string> GetGoalTemplateAsync(long id, CancellationToken cancellationToken);
    }

    public class GoalTemplateRepository : IGoalTemplateRepository
    {
        private readonly AppDbContext dbContext;

        public GoalTemplateRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> GetGoalTemplateAsync(long id, CancellationToken cancellationToken)
        {
            var goalTemplate = await dbContext.GoalTemplates
                .Where(x => x.Id == id)
                .Select(x => x.Description)
                .FirstOrDefaultAsync(cancellationToken);

            if(goalTemplate != null)
            {
                return goalTemplate;
            }

            return "Привести вес своего тела в {targetWeight}кг через {durationInDays} дней";
        }
    }
}
