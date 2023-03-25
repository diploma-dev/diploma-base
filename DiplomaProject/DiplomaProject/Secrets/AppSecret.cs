using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DiplomaProject.Secrets
{
    public static class AppSecret
    {
        public static class OpenAISettings
        {
            public static string OpenAIKey = "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";
        }

        public static class AuthSecret
        {
            public const string ISSUER = "AppAuthServer";
            public const string AUDIENCE = "AppAuthClient";
            private const string KEY = "gaWx9merTgX8kxX80wPpgGW19DpYCpOG";
            public const int ACCESSTOKENLIFETIME = 2880;
            public const int REFRESHTOKENLIFETIME = 43200;

            public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        }

        public static class PhotoSecret
        {
            public const string ProfilePhotoPath = "/ProfilePhotos/";
        }
    }
}
