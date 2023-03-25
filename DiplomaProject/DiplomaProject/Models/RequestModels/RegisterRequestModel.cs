using Microsoft.AspNetCore.Identity;

namespace DiplomaProject.Models.RequestModels
{
    public class RegisterRequestModel
    {
        public string Firstname { get; set; } = default!;
        public string Lastname { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string ConfirmPassword { get; set; } = default!;
        
    }
}
