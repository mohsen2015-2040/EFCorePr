using EFCorePr.Models;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EFCorePr.FastEndpoints.Book.GetById
{
    internal sealed class Endpoint :  Endpoint<BookGetByIdRequest>
    {
        private readonly BookStoreEFCoreContext _context;

        public Endpoint(BookStoreEFCoreContext context) => _context = context;

        public override void Configure()
        {
            Get("MyLibrary/books/search/{Id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(BookGetByIdRequest r, CancellationToken c)
        {
            var selectedBook = await _context.Book.FirstOrDefaultAsync(b => b.Id == r.Id && !b.IsDeleted);

            if (selectedBook == null) { await SendNotFoundAsync(); return; }
            
            await SendAsync(new
            {
                Title = selectedBook.Title,
                Isbn = selectedBook.Isbn,
                Description = selectedBook.Description,
                PublisherName = selectedBook.Publisher.FullName
            });
        }
    }
}