using EFCorePr.ViewModels.Book.Update;
using FluentValidation;

namespace EFCorePr.Validations
{
    public class UpdateBookValidator : AbstractValidator<UpdateBookViewModel>
    {
        public UpdateBookValidator()
        {
            RuleFor(b => b.Isbn).NotEmpty().WithMessage("Isbn Can Not Be Empty!");

            RuleFor(b => b.Title).NotEmpty().WithMessage("Title Can Not Be Empty!");
        }
    }
}
