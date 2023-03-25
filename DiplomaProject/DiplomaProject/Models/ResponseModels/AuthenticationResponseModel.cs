namespace DiplomaProject.Models.ResponseModels
{
    public class AuthenticationResponseModel
    {
        public long UserId { get; set; }
        public string AccessToken { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
        public string Error { get; set; } = default!;
    }
}
