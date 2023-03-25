namespace DiplomaProject.EntityModels
{
    public class ProfilePhotoEntity : BaseEntity
    {
        public string PhotoName { get; set; } = default!;
        public string PhotoFullPath { get; set; } = default!;
        public long UserId { get; set; } = default!;

        public virtual UserEntity User { get; set; } = default!;
    }
}
