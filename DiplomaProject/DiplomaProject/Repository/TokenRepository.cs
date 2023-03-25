using AutoMapper;
using DiplomaProject.DatabaseSecret;
using DiplomaProject.EntityModels;
using DiplomaProject.Models.AuthHelpers;
using Microsoft.EntityFrameworkCore;

namespace DiplomaProject.Repository
{
    public interface ITokenRepository
    {
        Task CreateAsync(RefreshTokenModel token, CancellationToken cancellationToken);
        Task<RefreshTokenModel> GetRefreshTokenAsync(string token, CancellationToken cancellationToken);
        Task<RefreshTokenModel> GetRefreshTokenAsync(long userId, CancellationToken cancellationToken);
        Task DeleteRefreshTokenAsync(long id, CancellationToken cancellationToken);
    }

    public class TokenRepository : ITokenRepository
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;

        public TokenRepository(AppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task CreateAsync(RefreshTokenModel token, CancellationToken cancellationToken)
        {
            var refreshToken = new RefreshTokenEntity()
            {
                Token = token.Token,
                UserId = token.UserId
            };

            await dbContext.RefreshTokens.AddAsync(refreshToken, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteRefreshTokenAsync(long tokenId, CancellationToken cancellationToken)
        {
            var refreshToken = await dbContext.RefreshTokens.Where(tkn => tkn.Id == tokenId).FirstOrDefaultAsync(cancellationToken);

            if (refreshToken != null)
            {
                dbContext.RefreshTokens.Remove(refreshToken);

                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<RefreshTokenModel> GetRefreshTokenAsync(string token, CancellationToken cancellationToken)
        {
            var refreshToken = await dbContext.RefreshTokens.Where(tkn => tkn.Token == token).FirstOrDefaultAsync(cancellationToken);

            return mapper.Map<RefreshTokenModel>(refreshToken);
        }

        public async Task<RefreshTokenModel> GetRefreshTokenAsync(long userId, CancellationToken cancellationToken)
        {
            var refreshToken = await dbContext.RefreshTokens.Where(tkn => tkn.UserId == userId).FirstOrDefaultAsync(cancellationToken);

            return mapper.Map<RefreshTokenModel>(refreshToken);
        }
    }
}
