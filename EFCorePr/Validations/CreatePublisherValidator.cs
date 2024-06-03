using EFCorePr.ViewModels;
using EFCorePr.ViewModels.Publisher.Create;
using FluentValidation;

namespace EFCorePr.Validations
{
    public class CreatePublisherValidator : AbstractValidator<CreatePublisherViewModel>
    {
        public CreatePublisherValidator() 
        {
            RuleFor(p => p.FullName).NotEmpty().WithMessage("FullName Can Not Be Null!");
        }
    }
}
