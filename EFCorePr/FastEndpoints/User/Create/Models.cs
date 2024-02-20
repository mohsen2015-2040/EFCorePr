using FastEndpoints;
using FluentValidation;

namespace EFCorePr.FasteEndpoints.User.Create
{
    internal sealed class CreateUserViewModel
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string NationalCode { get; set; } = string.Empty;

        public string PhoneNum { get; set; } = string.Empty;
    }

    internal sealed class CreateUserValidator : Validator<CreateUserViewModel>
    {
        public CreateUserValidator()
        {
            RuleFor(u => u.NationalCode).Length(10).WithMessage("NationalCode Must Be 10 Characters!");
            RuleFor(u => u.NationalCode).NotEmpty().WithMessage("NationalCode Can Not Be Empty!");

            RuleFor(u => u.PhoneNum).NotEmpty().WithMessage("PhoneNumber Can Not Be Empty!");
            RuleFor(u => u.PhoneNum).Length(11).WithMessage("PhoneNumber Must Be 11 Characters!");

            RuleFor(u => u.FirstName).NotEmpty().WithMessage("FirstName Can Not Be Empty!");

            RuleFor(u => u.LastName).NotEmpty().WithMessage("LastName Can Not Be Empty!");
        }
    }

    internal sealed class CreateUserResponse
    {
        public string Message { get; set; } = string.Empty;
    }
}