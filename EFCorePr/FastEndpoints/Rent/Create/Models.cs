using FastEndpoints;
using FluentValidation;

namespace EFCorePr.FasteEndpoints.Rent.Create
{
    internal sealed class CreateRentViewModel
    {
        public string BookIsbn { get; set; } = string.Empty;
        public string CustomerNationalCode { get; set; } = string.Empty;
    }

    internal sealed class CreateRentValidator : Validator<CreateRentViewModel>
    {
        public CreateRentValidator()
        {
            RuleFor(r => r.BookIsbn).NotEmpty().WithMessage("Isbn Can Not Be Null!");

            RuleFor(r => r.CustomerNationalCode).NotEmpty().WithMessage("CustomerNationalCode Can Not Be Null!");

        }
    }

    internal sealed class CreateRentResponse
    {
        public string Message { get; set; } = string.Empty;
    }
}