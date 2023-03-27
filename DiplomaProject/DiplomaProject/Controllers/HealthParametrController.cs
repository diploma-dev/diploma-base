using DiplomaProject.Models.RequestModels;
using DiplomaProject.Models.ResponseModels;
using DiplomaProject.Services;
using DiplomaProject.Validator;
using Microsoft.AspNetCore.Mvc;
using OneOf;

namespace DiplomaProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthParametrController : ApiBaseController
    {
        private readonly IHealthParametrService healthParametrService;

        public HealthParametrController(IHealthParametrService healthParametrService)
        {
            this.healthParametrService = healthParametrService;
        }

        [HttpGet("healthparametrs")]
        public async Task<HealthParametrsResponseModel> GetHealthParametrs(CancellationToken cancellationToken)
        {
            var currentUserId = GetCurrentUserId();

            return await healthParametrService.GetUserHealthDataAsync(currentUserId, cancellationToken);
        }

        [HttpPost("healthparametrs")]
        public async Task<IActionResult> UploadHealthParametrs(UploadHealthParametrRequestModel requestModel, CancellationToken cancellationToken)
        {
            var validator = new UploadHealthParametrRequestModelValidator();
            var validationResult = await validator.ValidateAsync(requestModel, cancellationToken);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
                
            }

            return Ok(await healthParametrService.UploadUserHealthDataAsync(requestModel, cancellationToken));
        }

        [HttpPut("healthparametrs")]
        public async Task<HealthParametrsResponseModel> UpdateHealthParametrs(UpdateHealthParametrRequestModel requestModel, CancellationToken cancellationToken)
        {
            return await healthParametrService.UpdateUserHealthDataAsync(requestModel, cancellationToken);
        }


        [HttpGet("user/bmi")]
        public async Task<BMIResponseModel> GetUserBMI(CancellationToken cancellationToken)
        {
            var currentUserId = GetCurrentUserId();

            return await healthParametrService.GetUserBMIAsync(currentUserId, cancellationToken);
        }
    }
}
