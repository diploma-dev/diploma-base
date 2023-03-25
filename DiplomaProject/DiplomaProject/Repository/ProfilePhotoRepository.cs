using AutoMapper;
using DiplomaProject.DatabaseSecret;
using DiplomaProject.EntityModels;
using DiplomaProject.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace DiplomaProject.Repository
{
    public interface IProfilePhotoRepository
    {
        Task<ProfilePhotoDTO> GetPhotoByUserIdAsync(long userId, CancellationToken cancellationToken);
        Task<ProfilePhotoDTO> GetPhotoByIdAsync(long photoId, CancellationToken cancellationToken);
        Task<ProfilePhotoDTO> AddPhotoAsync(long userId, string photoName, string photoFullPath, CancellationToken cancellationToken);
        Task DeletePhotoAsync(long photoId, CancellationToken cancellationToken);
    }

    public class ProfilePhotoRepository : IProfilePhotoRepository
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;

        public ProfilePhotoRepository(AppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ProfilePhotoDTO> GetPhotoByUserIdAsync(long userId, CancellationToken cancellationToken)
        {
            var photo = await dbContext.ProfilePhotos.Where(x => x.UserId == userId).FirstOrDefaultAsync(cancellationToken);

            return mapper.Map<ProfilePhotoDTO>(photo);
        }

        public async Task<ProfilePhotoDTO> AddPhotoAsync(long userId, string photoName, string photoFullPath, CancellationToken cancellationToken)
        {
            var photo = new ProfilePhotoEntity()
            {
                UserId = userId,
                PhotoName = photoName,
                PhotoFullPath = photoFullPath,
            };

            await dbContext.ProfilePhotos.AddAsync(photo, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return mapper.Map<ProfilePhotoDTO>(photo);
        }

        public async Task DeletePhotoAsync(long photoId, CancellationToken cancellationToken)
        {
            var photo = await dbContext.ProfilePhotos.Where(x => x.Id == photoId).FirstOrDefaultAsync(cancellationToken);

            if(photo != null)
            {
                dbContext.ProfilePhotos.Remove(photo);
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<ProfilePhotoDTO> GetPhotoByIdAsync(long photoId, CancellationToken cancellationToken)
        {
            var photo = await dbContext.ProfilePhotos.Where(x => x.Id == photoId).FirstOrDefaultAsync(cancellationToken);

            return mapper.Map<ProfilePhotoDTO>(photo);
        }
    }
}
