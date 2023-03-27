using AutoMapper;
using DiplomaProject.DatabaseSecret;
using DiplomaProject.EntityModels;
using DiplomaProject.Models.DTO;

namespace DiplomaProject.Repository
{
    public interface IUserBMIRepository
    {
        Task<BMIHistoryDTO> AddBMIAsync(double bmi, long userId, CancellationToken cancellationToken);
    }

    public class UserBMIRepository : IUserBMIRepository
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;

        public UserBMIRepository(AppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<BMIHistoryDTO> AddBMIAsync(double bmi, long userId, CancellationToken cancellationToken)
        {
            var bmiHistory = new BMIHistoryEntity()
            {
                BMI = bmi,
                UserId = userId,
                CheckDate = DateOnly.FromDateTime(DateTime.Now)
            };

            await dbContext.BMIHistories.AddAsync(bmiHistory, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return mapper.Map<BMIHistoryDTO>(bmiHistory);
        }
    }
}
