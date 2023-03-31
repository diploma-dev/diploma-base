using DiplomaProject.Models.RequestModels;
using DiplomaProject.Models.ResponseModels;
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
        public async Task<ProfileResponseModel> GetUserProfile(CancellationToken cancellationToken)
        {
            var currentUserId = GetCurrentUserId();

            return await profileService.GetUserProfileAsync(currentUserId, cancellationToken);
        }

        [HttpGet("photo")]
        public async Task<ProfilePhotoResponseModel> GetProfilePhoto(CancellationToken cancellationToken)
        {
            var currentUserId = GetCurrentUserId();

            return await photoService.GetProfilePhotoAsync(currentUserId, cancellationToken);
        }

        [HttpPost("photo")]
        public async Task<ProfilePhotoResponseModel> UploadProfilePhoto([FromForm] UploadProfilePhotoRequestModel requestModel, CancellationToken cancellationToken)
        {
            var currentUserId = GetCurrentUserId();

            return await photoService.UploadProfilePhotoAsync(currentUserId, requestModel.File, cancellationToken);
        }

        [HttpDelete("photo")]
        public async Task DeleteProfilePhoto(long photoId, CancellationToken cancellationToken)
        {
            await photoService.DeleteProfilePhotoAsync(photoId, cancellationToken);
        }
    }
}
