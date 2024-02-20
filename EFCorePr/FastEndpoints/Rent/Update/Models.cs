using FastEndpoints;
using FluentValidation;

namespace EFCorePr.FasteEndpoints.Rent.Update
{
    internal sealed class UpdateRentViewModel
    {
        public int Id { get; set; }
        public string BookIsbn { get; set; } = string.Empty;
        public string CustomerNationalCode { get; set; } = string.Empty;
        public DateTime EndDate { get; set; }
    }

    internal sealed class UpdateRentValidator : Validator<UpdateRentViewModel>
    {
        public UpdateRentValidator()
        {
            RuleFor(r => r.BookIsbn).NotEmpty().WithMessage("Isbn Can Not Be Null!");

            RuleFor(r => r.CustomerNationalCode).NotEmpty().WithMessage("CustomerNationalCode Can Not Be Null!");

            RuleFor(r => r.EndDate).NotEmpty().WithMessage("EndDate Can Not Be Null!");
        }
    }

    internal sealed class UpdateRentResponse
    {
        public string Message { get; set; } = string.Empty;
    }
}