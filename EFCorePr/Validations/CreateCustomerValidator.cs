
using EFCorePr.ViewModels;
using EFCorePr.ViewModels.Customer.Create;
using FluentValidation;

namespace EFCorePr.Validations
{
    public class CreateCustomerValidator : AbstractValidator<CreateCustomerViewModel>
    {
        public CreateCustomerValidator()
        {
            RuleFor(u => u.NationalCode).Length(10).WithMessage("NationalCode Must Be 10 Characters!");
            RuleFor(u => u.NationalCode).NotEmpty().WithMessage("NationalCode Can Not Be Empty!");

            RuleFor(u => u.PhoneNum).NotEmpty().WithMessage("PhoneNumber Can Not Be Empty!");
            RuleFor(u => u.PhoneNum).Length(11).WithMessage("PhoneNumber Must Be 11 Characters!");

            RuleFor(u => u.FirstName).NotEmpty().WithMessage("FirstName Can Not Be Empty!");

            RuleFor(u => u.LastName).NotEmpty().WithMessage("LastName Can Not Be Empty!");

            RuleFor(u => u.Password).NotEmpty().MinimumLength(4).WithMessage("Password must be at least 4 Chacters!");
        }
    }
}
