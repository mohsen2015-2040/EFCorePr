using EFCorePr.ViewModels.Publisher.Update;
using FluentValidation;

namespace EFCorePr.Validations
{
    public class UpdatePublisherValidator : AbstractValidator<UpdatePublisherViewmodel>
    {
        public UpdatePublisherValidator() 
        {
            RuleFor(p => p.FullName).NotEmpty().WithMessage("FullName Can Not Be Null!");
        }
    }
}
