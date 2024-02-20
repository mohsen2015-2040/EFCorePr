using FluentValidation;

namespace EFCorePr.FastEndpoints.Book.Create
{
    internal sealed class CreateBookViewModel
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Isbn { get; set; } = string.Empty;
        public string PublisherName { get; set; } = string.Empty;
    }

    internal sealed class CreateBookValidator : AbstractValidator<CreateBookViewModel>
    {
        public CreateBookValidator()
        {
            RuleFor(b => b.Isbn).NotEmpty().WithMessage("Isbn Can Not Be Empty!");

            RuleFor(b => b.Title).NotEmpty().WithMessage("Title Can Not Be Empty!");
        }
    }

    internal sealed class CreateBookResponse
    {
        public string Message { get; set; } = string.Empty;
    }
}