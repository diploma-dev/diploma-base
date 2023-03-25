namespace DiplomaProject.Models.AuthHelpers
{
    public class PasswordHashModel
    {
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
