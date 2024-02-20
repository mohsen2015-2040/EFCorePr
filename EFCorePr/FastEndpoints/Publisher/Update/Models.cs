using FastEndpoints;
using FluentValidation;

namespace EFCorePr.FasteEndpoints.Publisher.Update
{
    internal sealed class UpdatePublisherViewModel
    {
        public int Id { get; set; }
        public string FullName {  get; set; } = string.Empty;
    }

    internal sealed class UpdatePublisherValidator : Validator<UpdatePublisherViewModel>
    {
        public UpdatePublisherValidator()
        {
            RuleFor(p => p.FullName).NotEmpty().WithMessage("FullName Can Not Be Null!");
        }
    }

    internal sealed class UpdatePublisherResponse
    {
        public string Message { get; set; } = string.Empty;
    }
}