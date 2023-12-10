using EFCorePr.ViewModels;
using FluentValidation;

namespace EFCorePr.Validations
{
    public class PublisherValidator : AbstractValidator<PublisherViewData>
    {
        public PublisherValidator() 
        {
            RuleFor(p => p.FullName).NotEmpty().WithMessage("FullName Can Not Be Null!");
        }
    }
}
