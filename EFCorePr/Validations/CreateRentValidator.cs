using EFCorePr.ViewModels;
using EFCorePr.ViewModels.Rent.Create;
using EFCorePr.ViewModels.Rent.Update;
using FluentValidation;

namespace EFCorePr.Validations
{
    public class CreateRentValidator : AbstractValidator<CreateRentViewModel>
    {
        public CreateRentValidator() 
        {
            RuleFor(r => r.BookIsbn).NotEmpty().WithMessage("Isbn Can Not Be Null!");

            RuleFor(r => r.CustomerNationalCode).NotEmpty().WithMessage("CustomerNationalCode Can Not Be Null!");

            RuleFor(r => r.EndDate).NotEmpty().WithMessage("EndDate Can Not Be Null!");
        }
    }
}
