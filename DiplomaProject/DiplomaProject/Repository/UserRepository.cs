using AutoMapper;
using DiplomaProject.DatabaseSecret;
using DiplomaProject.EntityModels;
using DiplomaProject.EntityModels.Enums;
using DiplomaProject.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace DiplomaProject.Repository
{
    public interface IUserRepository
    {
        Task<UserDTO> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<UserDTO> GetByIdAsync(long userId, CancellationToken cancellationToken);
        Task<UserDTO> AddUserAsync(UserDTO userDTO, CancellationToken cancellationToken);
    }

    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;

        public UserRepository(AppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<UserDTO> AddUserAsync(UserDTO userDTO, CancellationToken cancellationToken)
        {
            var user = new UserEntity()
            {
                Firstname = userDTO.Firstname,
                Lastname = userDTO.Lastname,
                Email = userDTO.Email,
                Role = UserRole.BaseUser.ToString(),
                PasswordHash = userDTO.PasswordHash,
                PasswordSalt = userDTO.PasswordSalt,
            };

            await dbContext.Users.AddAsync(user, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            mapper.Map(user, userDTO);

            return userDTO;
        }

        public async Task<UserDTO> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var user = await dbContext.Users.Where(x => x.Email == email).FirstOrDefaultAsync(cancellationToken);

            return mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> GetByIdAsync(long userId, CancellationToken cancellationToken)
        {
            var user = await dbContext.Users.Where(x => x.Id == userId).FirstOrDefaultAsync(cancellationToken);

            return mapper.Map<UserDTO>(user);
        }
    }
}
