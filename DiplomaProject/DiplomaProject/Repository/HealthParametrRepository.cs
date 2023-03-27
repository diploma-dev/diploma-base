using AutoMapper;
using DiplomaProject.DatabaseSecret;
using DiplomaProject.EntityModels;
using DiplomaProject.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace DiplomaProject.Repository
{
    public interface IHealthParametrRepository
    {
        Task<HealthParametrDTO> GetHealthParametrAsync(long userId, CancellationToken cancellationToken);
        Task<HealthParametrDTO> UpdateHealthParametrAsync(HealthParametrDTO healthParametr, CancellationToken cancellationToken);
        Task<HealthParametrDTO> UploadHealthParametrAsync(HealthParametrDTO healthParametr, CancellationToken cancellationToken);
    }

    public class HealthParametrRepository : IHealthParametrRepository
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;

        public HealthParametrRepository(AppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<HealthParametrDTO> GetHealthParametrAsync(long userId, CancellationToken cancellationToken)
        {
            var userHealthParametrs = await dbContext.HealthParametrs.Where(x => x.UserId == userId).FirstOrDefaultAsync(cancellationToken);
            
            return mapper.Map<HealthParametrDTO>(userHealthParametrs);
        }

        public async Task<HealthParametrDTO> UpdateHealthParametrAsync(HealthParametrDTO healthParametrDTO, CancellationToken cancellationToken)
        {
            var healthParametr = await dbContext.HealthParametrs.Where(x => x.UserId == healthParametrDTO.UserId).FirstOrDefaultAsync(cancellationToken);

            if(healthParametr != null)
            {
                healthParametr.Age = healthParametrDTO.Age;
                healthParametr.Weight = healthParametrDTO.Weight;
                healthParametr.Height = healthParametrDTO.Height;

                await dbContext.SaveChangesAsync(cancellationToken);
            }

            mapper.Map(healthParametr, healthParametrDTO);

            return healthParametrDTO;
        }

        public async Task<HealthParametrDTO> UploadHealthParametrAsync(HealthParametrDTO healthParametrDTO, CancellationToken cancellationToken)
        {
            var userHealthParametrs = await dbContext.HealthParametrs.Where(x => x.UserId == healthParametrDTO.UserId).FirstOrDefaultAsync(cancellationToken);

            if(userHealthParametrs != null)
            {
                dbContext.HealthParametrs.Remove(userHealthParametrs);
                await dbContext.SaveChangesAsync(cancellationToken);
            }

            var healthParametr = new HealthParametrEntity()
            {
                UserId = healthParametrDTO.UserId,
                Gender = healthParametrDTO.Gender,
                Age = healthParametrDTO.Age,
                Height = healthParametrDTO.Height,
                Weight = healthParametrDTO.Weight,
            };

            await dbContext.HealthParametrs.AddAsync(healthParametr, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            mapper.Map(healthParametr, healthParametrDTO);

            return healthParametrDTO;
        }
    }
}
