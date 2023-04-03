using AutoMapper;
using DiplomaProject.Models.AuthHelpers;
using DiplomaProject.Models.DTO;
using DiplomaProject.Models.RequestModels;
using DiplomaProject.Models.ResponseModels;
using DiplomaProject.Repository;
using DiplomaProject.Secrets;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.IdentityModel.Tokens.Jwt;

namespace DiplomaProject.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponseModel> RegisterAsync(RegisterRequestModel model, CancellationToken cancellationToken);
        Task<AuthenticationResponseModel> LoginAsync(LoginRequestModel model, CancellationToken cancellationToken);
        Task<AuthenticationResponseModel> RefreshAsync(string token, CancellationToken cancellationToken);
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthHelper authHelper;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly ITokenRepository tokenRepository;

        public AuthenticationService(IAuthHelper authHelper, IUserRepository userRepository, IMapper mapper, ITokenRepository tokenRepository)
        {
            this.authHelper = authHelper;
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.tokenRepository = tokenRepository;
        }

        public async Task<AuthenticationResponseModel> LoginAsync(LoginRequestModel model, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByEmailAsync(model.Email, cancellationToken);

            if(user == null)
            {
                return new AuthenticationResponseModel()
                {
                    Error = "User is not exist."
                };
            }

            var isPasswordCorrecrt = authHelper.VerifyPasswordhash(model.Password, user.PasswordHash, user.PasswordSalt);

            if (!isPasswordCorrecrt)
            {
                return new AuthenticationResponseModel()
                {
                    Error = "Invalid password."
                };
            }

            var expiredToken = await tokenRepository.GetRefreshTokenAsync(user.Id, cancellationToken);
            await tokenRepository.DeleteRefreshTokenAsync(expiredToken.Id, cancellationToken);

            var accessToken = authHelper.GenerateAccessToken(user);
            var refreshToken = authHelper.GenerateRefreshToken();

            await tokenRepository.CreateAsync(new RefreshTokenModel { UserId = user.Id, Token = refreshToken }, cancellationToken);

            return new AuthenticationResponseModel()
            {
                UserId = user.Id,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }

        public async Task<AuthenticationResponseModel> RefreshAsync(string token, CancellationToken cancellationToken)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            TokenValidationParameters parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = AppSecret.AuthSecret.ISSUER,
                ValidateAudience = true,
                ValidAudience = AppSecret.AuthSecret.AUDIENCE,
                ValidateLifetime = true,
                IssuerSigningKey = AppSecret.AuthSecret.GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                handler.ValidateToken(token, parameters, out SecurityToken validatedToken);
            }
            catch (Exception ex)
            {
                return new AuthenticationResponseModel()
                {
                    Error = $"{ex.Message}"
                };
            }

            var refreshToken = await tokenRepository.GetRefreshTokenAsync(token, cancellationToken);

            if (refreshToken == null)
            {
                return new AuthenticationResponseModel()
                {
                    Error = "Invalid refresh token"
                };
            }

            await tokenRepository.DeleteRefreshTokenAsync(refreshToken.Id, cancellationToken);

            var currentUser = await userRepository.GetByIdAsync(refreshToken.UserId, cancellationToken);

            if (currentUser == null)
            {
                return new AuthenticationResponseModel()
                {
                    Error = "User not found"
                };
            }

            var currentAccessToken = authHelper.GenerateAccessToken(currentUser);
            var currentRefreshToken = authHelper.GenerateRefreshToken();

            await tokenRepository.CreateAsync(new RefreshTokenModel { Token = currentRefreshToken, UserId = currentUser.Id }, cancellationToken);

            return new AuthenticationResponseModel()
            {
                UserId = currentUser.Id,
                RefreshToken = currentRefreshToken,
                AccessToken = currentAccessToken
            };
        }
    

        public async Task<AuthenticationResponseModel> RegisterAsync(RegisterRequestModel model, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByEmailAsync(model.Email, cancellationToken);

            if (user != null)
            {
                return new AuthenticationResponseModel() { Error = "User already exist"};
            }

            var hashModel = authHelper.CreatePasswordHash(model.Password);

            var userDTO = mapper.Map<UserDTO>(model);

            userDTO.PasswordSalt = hashModel.PasswordSalt;
            userDTO.PasswordHash = hashModel.PasswordHash;

            userDTO = await userRepository.AddUserAsync(userDTO, cancellationToken);

            var accessToken = authHelper.GenerateAccessToken(userDTO);
            var refreshToken = authHelper.GenerateRefreshToken();

            await tokenRepository.CreateAsync(new RefreshTokenModel { Token = refreshToken, UserId = userDTO.Id }, cancellationToken);

            return new AuthenticationResponseModel
            {
                UserId = userDTO.Id,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }
    }
}
