using FastEndpoints;
using FluentValidation;

namespace EFCorePr.FasteEndpoints.Book.Update
{
    internal sealed class UpdateBookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Isbn { get; set; } = string.Empty;
        public string PublisherName { get; set; } = string.Empty;
    }

    internal sealed class UpdateBookValidator : AbstractValidator<UpdateBookViewModel>
    {
        public UpdateBookValidator()
        {
            RuleFor(b => b.Isbn).NotEmpty().WithMessage("Isbn Can Not Be Empty!");

            RuleFor(b => b.Title).NotEmpty().WithMessage("Title Can Not Be Empty!");
        }
    }

    internal sealed class UpdateBookResponse
    {
        public string Message { get; set; } = string.Empty;
    }
}