using DiplomaProject.Models.RequestModels;
using DiplomaProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ApiBaseController
    {
        private readonly IProfileService profileService;
        private readonly IProfilePhotoService photoService;

        public ProfileController(IProfileService profileService, IProfilePhotoService photoService)
        {
            this.profileService = profileService;
            this.photoService = photoService;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetUserProfile(CancellationToken cancellationToken)
        {
            var currentUserId = GetCurrentUserId();

            return Ok(await profileService.GetUserProfileAsync(currentUserId, cancellationToken));
        }

        [HttpGet("photo")]
        public async Task<IActionResult> GetProfilePhoto(CancellationToken cancellationToken)
        {
            var currentUserId = GetCurrentUserId();

            return Ok(await photoService.GetProfilePhotoAsync(currentUserId, cancellationToken));
        }

        [HttpPost("photo")]
        public async Task<IActionResult> UploadProfilePhoto([FromForm] UploadProfilePhotoRequestModel requestModel, CancellationToken cancellationToken)
        {
            var currentUserId = GetCurrentUserId();

            return Ok(await photoService.UploadProfilePhotoAsync(currentUserId, requestModel.File, cancellationToken));
        }

        [HttpDelete("photo")]
        public async Task DeleteProfilePhoto(long photoId, CancellationToken cancellationToken)
        {
            await photoService.DeleteProfilePhotoAsync(photoId, cancellationToken);
        }
    }
}
