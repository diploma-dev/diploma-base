using AutoMapper;
using DiplomaProject.DatabaseSecret;
using DiplomaProject.EntityModels;
using DiplomaProject.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace DiplomaProject.Repository
{
    public interface IProfilePhotoRepository
    {
        Task<ProfilePhotoDTO> GetPhotoByUserIdAsync(long id, CancellationToken cancellationToken);
        Task<ProfilePhotoDTO> GetPhotoByIdAsync(long id, CancellationToken cancellationToken);
        Task<ProfilePhotoDTO> AddPhotoAsync(long id, string photoName, string photoFullPath, CancellationToken cancellationToken);
        Task DeletePhotoAsync(long id, CancellationToken cancellationToken);
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

        public async Task<ProfilePhotoDTO> GetPhotoByUserIdAsync(long id, CancellationToken cancellationToken)
        {
            var photo = await dbContext.ProfilePhotos.Where(x => x.UserId == id).FirstOrDefaultAsync(cancellationToken);

            return mapper.Map<ProfilePhotoDTO>(photo);
        }

        public async Task<ProfilePhotoDTO> AddPhotoAsync(long id, string photoName, string photoFullPath, CancellationToken cancellationToken)
        {
            var photo = new ProfilePhotoEntity()
            {
                UserId = id,
                PhotoName = photoName,
                PhotoFullPath = photoFullPath,
            };

            await dbContext.ProfilePhotos.AddAsync(photo, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return mapper.Map<ProfilePhotoDTO>(photo);
        }

        public async Task DeletePhotoAsync(long id, CancellationToken cancellationToken)
        {
            var photo = await dbContext.ProfilePhotos.Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);

            if(photo != null)
            {
                dbContext.ProfilePhotos.Remove(photo);
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<ProfilePhotoDTO> GetPhotoByIdAsync(long id, CancellationToken cancellationToken)
        {
            var photo = await dbContext.ProfilePhotos.Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);

            return mapper.Map<ProfilePhotoDTO>(photo);
        }
    }
}
