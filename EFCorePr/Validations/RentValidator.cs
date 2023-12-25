using EFCorePr.ViewModels.Rent;
using FluentValidation;

namespace EFCorePr.Validations
{
    public class RentValidator : AbstractValidator<RentViewData>
    {
        public RentValidator() 
        {
            RuleFor(r => r.BookIsbn).NotEmpty().WithMessage("Isbn Can Not Be Null!");

            RuleFor(r => r.CustomerNationalCode).NotEmpty().WithMessage("CustomerNationalCode Can Not Be Null!");

            RuleFor(r => r.StartDate).NotEmpty().WithMessage("StartDate Can Not Be Null!");

            RuleFor(r => r.EndDate).NotEmpty().WithMessage("EndDate Can Not Be Null!");
        }
    }
}
