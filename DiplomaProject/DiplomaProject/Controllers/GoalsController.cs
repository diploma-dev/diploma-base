using DiplomaProject.Models.RequestModels;
using DiplomaProject.Models.ResponseModels;
using DiplomaProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalsController : ApiBaseController
    {
        private readonly IGoalService goalService;

        public GoalsController(IGoalService goalService)
        {
            this.goalService = goalService;
        }

        [HttpGet("goal")]
        public async Task<GoalResponseModel> GetGoal(CancellationToken cancellationToken)
        {
            var currentUser = GetCurrentUserId();

            return await goalService.GetGoalAsync(currentUser, cancellationToken);
        }

        [HttpPost("goal")]
        public async Task<GoalResponseModel> CreateGoal(CreateGoalRequestModel requestModel,CancellationToken cancellationToken)
        {
            return await goalService.CreateGoalAsync(requestModel, cancellationToken);
        }

        [HttpDelete("goal")]
        public async Task DeleteGoal(long goalId, CancellationToken cancellationToken)
        {
            await goalService.DeleteGoalAsync(goalId, cancellationToken);
        }
    }
}
