using DiplomaProject.Models.RequestModels;
using FluentValidation;

namespace DiplomaProject.Validator
{
    public class LoginRequestModelValidator : AbstractValidator<LoginRequestModel>
    {
        public LoginRequestModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).MaximumLength(50);
        }
    }
}
