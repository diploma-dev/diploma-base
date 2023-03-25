using DiplomaProject.Models.RequestModels;
using DiplomaProject.Services;
using DiplomaProject.Validator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ApiBaseController
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticateController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [AllowAnonymous, HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestModel requestModel, CancellationToken cancellationToken)
        {
            var validator = new RegisterRequestModelValidator();
            var validationResult = await validator.ValidateAsync(requestModel, cancellationToken);

            if (validationResult.IsValid)
            {
                return Ok(await authenticationService.RegisterAsync(requestModel, cancellationToken));
            }

            return BadRequest();
        }

        [AllowAnonymous, HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel requestModel, CancellationToken cancellationToken)
        {
            var validator = new LoginRequestModelValidator();
            var validationResult = await validator.ValidateAsync(requestModel, cancellationToken);

            if (validationResult.IsValid)
            {
                return Ok(await authenticationService.LoginAsync(requestModel, cancellationToken));
            }

            return BadRequest();
        }

        [AllowAnonymous, HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] string token, CancellationToken cancellationToken)
        {
            return Ok(await authenticationService.RefreshAsync(token, cancellationToken));
        }
    }
}
