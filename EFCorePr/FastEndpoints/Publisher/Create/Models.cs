using FastEndpoints;
using FluentValidation;

namespace EFCorePr.FasteEndpoints.Publisher.Create
{
    internal sealed class CreatePublisherViewModel
    {
        public string FullName {  get; set; } = string.Empty;
    }

    internal sealed class CreatePublisherValidator : Validator<CreatePublisherViewModel>
    {
        public CreatePublisherValidator()
        {
            RuleFor(p => p.FullName).NotEmpty().WithMessage("FullName Can Not Be Null!");
        }
    }

    internal sealed class CreatePublisherResponse
    {
        public string Message { get; set; } = string.Empty;
    }
}