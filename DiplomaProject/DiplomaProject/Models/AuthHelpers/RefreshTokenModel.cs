namespace DiplomaProject.Models.AuthHelpers
{
    public class RefreshTokenModel
    {
        public long Id { get; set; } = default!;
        public string Token { get; set; } = default!;
        public long UserId { get; set; } = default!;
    }
}
