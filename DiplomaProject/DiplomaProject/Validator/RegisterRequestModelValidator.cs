using DiplomaProject.Models.RequestModels;
using FluentValidation;

namespace DiplomaProject.Validator
{
    public class RegisterRequestModelValidator : AbstractValidator<RegisterRequestModel>
    {
        public RegisterRequestModelValidator()
        {
            RuleFor(x => x.Firstname).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Lastname).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).MaximumLength(50);
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);
        }
    }
}
