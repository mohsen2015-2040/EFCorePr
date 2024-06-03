using EFCorePr.Models;
using EFCorePr.ViewModels;
using EFCorePr.ViewModels.Book.Create;
using FluentValidation;

namespace EFCorePr.Validations
{
    public class CreateBookValidator : AbstractValidator<CreateBookViewModel>
    {
        public CreateBookValidator() 
        {
            RuleFor(b => b.Isbn).NotEmpty().WithMessage("Isbn Can Not Be Empty!");

            RuleFor(b => b.Title).NotEmpty().WithMessage("Title Can Not Be Empty!");
        }
    }
}
