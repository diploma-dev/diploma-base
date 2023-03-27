using DiplomaProject.Models.RequestModels;
using FluentValidation;

namespace DiplomaProject.Validator
{
    public class UploadHealthParametrRequestModelValidator : AbstractValidator<UploadHealthParametrRequestModel>
    {
        public UploadHealthParametrRequestModelValidator()
        {
            RuleFor(x => x.Age).NotEmpty().LessThan(100);
            RuleFor(x => x.Weight).NotEmpty();
            RuleFor(x => x.Height).NotNull();
        }
    }
}
