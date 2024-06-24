using EFCorePr.ViewModels.Customer.Update;
using FluentValidation;

namespace EFCorePr.Validations
{
    public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerViewModel>
    {
        public UpdateCustomerValidator() 
        {
            RuleFor(u => u.PhoneNum).NotEmpty().WithMessage("PhoneNumber Can Not Be Empty!");
            RuleFor(u => u.PhoneNum).Length(11).WithMessage("PhoneNumber Must Be 11 Characters!");

            RuleFor(u => u.FirstName).NotEmpty().WithMessage("FirstName Can Not Be Empty!");

            RuleFor(u => u.LastName).NotEmpty().WithMessage("LastName Can Not Be Empty!");
        }
    }
}
