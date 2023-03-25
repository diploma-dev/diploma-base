namespace DiplomaProject.Models.DTO
{
    public class ProfilePhotoDTO
    {
        public long Id { get; set; }
        public string PhotoName { get; set; } = default!;
        public string PhotoFullPath { get; set; } = default!;
        public long UserId { get; set; } = default!;
    }
}
