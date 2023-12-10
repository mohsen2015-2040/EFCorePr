using EFCorePr.Models;
using EFCorePr.ViewModels;
using FluentValidation;

namespace EFCorePr.Validations
{
    public class BookValidator : AbstractValidator<BookViewData>
    {
        public BookValidator() 
        {
            RuleFor(b => b.Isbn).NotEmpty().WithMessage("Isbn Can Not Be Empty!");

            RuleFor(b => b.Title).NotEmpty().WithMessage("Title Can Not Be Empty!");
        }
    }
}
