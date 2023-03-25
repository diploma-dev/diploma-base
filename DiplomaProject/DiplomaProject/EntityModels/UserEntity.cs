namespace DiplomaProject.EntityModels
{
    public class UserEntity : BaseEntity
    {
        public string Firstname { get; set; } = default!;
        public string Lastname { get; set; } = default!;
        public string Email { get; set; } = default!;

        public byte[] PasswordHash { get; set; } = default!;
        public byte[] PasswordSalt { get; set; } = default!;

        public virtual ProfilePhotoEntity ProfilePhoto { get; set; } = default!;
    }
}
