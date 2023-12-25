using EFCorePr.Models;
using EFCorePr.ViewModels.Book;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace EFCorePr.FastEndpoints
{
    public class CreateBook : Endpoint<AddBookViewModel>
    {
        private readonly BookStoreEFCoreContext _dbContext;
       // private AddBookViewModel _AddBookView;

        public CreateBook(BookStoreEFCoreContext context/*, AddBookViewModel viewModel*/) 
        {
            _dbContext = context;
            //_AddBookView = viewModel;
        }

        public override void Configure()
        {
            Post("/Books/add");
            AllowAnonymous();
        }

        public override async Task HandleAsync(AddBookViewModel req, CancellationToken ct)
        {
            var BookViewPublisher = await _dbContext.Publisher.FirstOrDefaultAsync(p => p.FullName == req.PublisherName && !p.IsDeleted);

            if(BookViewPublisher == null)
            {
                await SendOkAsync("Invalid Publisher!");
                return;
            }

            if (!ValidationFailed)
            {
                _dbContext.Add(new Book
                {
                    Title = req.Title,
                    Description = req.Description,
                    Isbn = req.Isbn,
                    PublisherId = BookViewPublisher.Id
                });
                
                await SendOkAsync("Successfully Added");
                return;
            }
            
            await SendOkAsync(req);
        }
    }
}
