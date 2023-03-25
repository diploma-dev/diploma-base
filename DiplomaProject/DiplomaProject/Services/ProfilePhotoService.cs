using AutoMapper;
using DiplomaProject.EntityModels;
using DiplomaProject.Models.ResponseModels.ProfilePhoto;
using DiplomaProject.Repository;
using DiplomaProject.Secrets;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace DiplomaProject.Services
{
    public interface IProfilePhotoService
    {
        Task<ProfilePhotoResponseModel> UploadProfilePhotoAsync(long userId, IFormFile photo, CancellationToken cancellationToken);
        Task DeleteProfilePhotoAsync(long photoId, CancellationToken cancellationToken);
        Task<ProfilePhotoResponseModel> GetProfilePhotoAsync(long userId, CancellationToken cancellationToken);
    }

    public class ProfilePhotoService : IProfilePhotoService
    {
        private readonly IProfilePhotoRepository profilePhotoRepository;
        private readonly IUserRepository userRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IMapper mapper;

        public ProfilePhotoService(IProfilePhotoRepository profilePhotoRepository, IUserRepository userRepository, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            this.profilePhotoRepository = profilePhotoRepository;
            this.userRepository = userRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.mapper = mapper;
        }

        public async Task<ProfilePhotoResponseModel> UploadProfilePhotoAsync(long userId, IFormFile photo, CancellationToken cancellationToken)
        {
            var currentProfilePhoto = await profilePhotoRepository.GetPhotoByUserIdAsync(userId, cancellationToken);
            var user = await userRepository.GetByIdAsync(userId, cancellationToken);

            var profilePhotoName = user.Firstname + user.Lastname + userId.ToString() + ".jpg";
            var profilePhotoFullPath = AppSecret.PhotoSecret.ProfilePhotoPath + profilePhotoName;

            if (currentProfilePhoto == null)
            {
                using (var fileStream = new FileStream(webHostEnvironment.WebRootPath + profilePhotoFullPath, FileMode.Create))
                {
                    await photo.CopyToAsync(fileStream);
                }

                var profilePhotoDTO = await profilePhotoRepository.AddPhotoAsync(userId, profilePhotoName, profilePhotoFullPath, cancellationToken);

                return new ProfilePhotoResponseModel()
                {
                    PhotoPath = profilePhotoDTO.PhotoFullPath
                };
            }
            else
            {
                System.IO.File.Delete(webHostEnvironment.WebRootPath + currentProfilePhoto.PhotoFullPath);

                await profilePhotoRepository.DeletePhotoAsync(currentProfilePhoto.Id, cancellationToken);

                using (var fileStream = new FileStream(webHostEnvironment.WebRootPath + profilePhotoFullPath, FileMode.Create))
                {
                    await photo.CopyToAsync(fileStream);
                }

                var profilePhotoDTO = await profilePhotoRepository.AddPhotoAsync(userId, profilePhotoName, profilePhotoFullPath, cancellationToken);

                return new ProfilePhotoResponseModel()
                {
                    PhotoPath = profilePhotoDTO.PhotoFullPath
                };
            }
        }

        public async Task DeleteProfilePhotoAsync(long photoId, CancellationToken cancellationToken)
        {
            var profilePhoto = await profilePhotoRepository.GetPhotoByIdAsync(photoId, cancellationToken);

            if(profilePhoto != null)
            {
                System.IO.File.Delete(webHostEnvironment.WebRootPath + profilePhoto.PhotoFullPath);

                await profilePhotoRepository.DeletePhotoAsync(profilePhoto.Id, cancellationToken);
            }
        }

        public async Task<ProfilePhotoResponseModel> GetProfilePhotoAsync(long userId, CancellationToken cancellationToken)
        {
            var profilePhoto = await profilePhotoRepository.GetPhotoByUserIdAsync(userId, cancellationToken);

            return mapper.Map<ProfilePhotoResponseModel>(profilePhoto);
        }
    }
}
