using AutoMapper;
using DiplomaProject.Models.ResponseModels;
using DiplomaProject.Repository;

namespace DiplomaProject.Services
{
    public interface IProfileService
    {
        Task<ProfileResponseModel> GetUserProfileAsync(long userId, CancellationToken cancellationToken);
    }

    public class ProfileService : IProfileService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public ProfileService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<ProfileResponseModel> GetUserProfileAsync(long userId, CancellationToken cancellationToken)
        {
            var userDTO = await userRepository.GetByIdAsync(userId, cancellationToken);

            if(userDTO != null)
            {
                return mapper.Map<ProfileResponseModel>(userDTO);
            }

            return new ProfileResponseModel();
        }
    }
}
