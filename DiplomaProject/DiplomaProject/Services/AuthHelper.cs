using DiplomaProject.Models.AuthHelpers;
using DiplomaProject.Models.DTO;
using DiplomaProject.Secrets;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace DiplomaProject.Services
{
    public interface IAuthHelper
    {
        PasswordHashModel CreatePasswordHash(string password);
        bool VerifyPasswordhash(string password, byte[] passwordHash, byte[] passwordSalt);
        string GenerateRefreshToken();
        string GenerateAccessToken(UserDTO user);
    }

    public class AuthHelper : IAuthHelper
    {
        public string GenerateAccessToken(UserDTO user)
        {
            var securityKey = AppSecret.AuthSecret.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Firstname),
                new Claim(ClaimTypes.Surname, user.Lastname),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Role", user.Role)
            };

            var token = new JwtSecurityToken(
                AppSecret.AuthSecret.ISSUER,
                AppSecret.AuthSecret.AUDIENCE,
                claims,
                expires: DateTime.Now.AddMinutes(2880),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public PasswordHashModel CreatePasswordHash(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                return new PasswordHashModel()
                {
                    PasswordSalt = hmac.Key,
                    PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password))
                };
            }
        }

        public bool VerifyPasswordhash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes((string)password));

                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public string GenerateRefreshToken()
        {
            var securityKey = AppSecret.AuthSecret.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                AppSecret.AuthSecret.ISSUER,
                AppSecret.AuthSecret.AUDIENCE,
                expires: DateTime.Now.AddMinutes(43200),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
