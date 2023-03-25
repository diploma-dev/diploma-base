namespace DiplomaProject.EntityModels
{
    public class RefreshTokenEntity : BaseEntity
    {
        public string Token { get; set; } = default!;
        public long UserId { get; set; } = default!;
    }
}
