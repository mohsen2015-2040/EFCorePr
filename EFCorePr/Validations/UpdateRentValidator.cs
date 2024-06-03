using EFCorePr.ViewModels.Rent.Update;
using FluentValidation;

namespace EFCorePr.Validations
{
    public class UpdateRentValidator:AbstractValidator<UpdateRentViewModel>
    {
        public UpdateRentValidator() 
        {
            RuleFor(r => r.BookIsbn).NotEmpty().WithMessage("Isbn Can Not Be Null!");

            RuleFor(r => r.CustomerNationalCode).NotEmpty().WithMessage("CustomerNationalCode Can Not Be Null!");

            RuleFor(r => r.EndDate).NotEmpty().WithMessage("EndDate Can Not Be Null!");
        }
    }
}
