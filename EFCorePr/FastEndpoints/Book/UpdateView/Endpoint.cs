using EFCorePr.Models;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EFCorePr.FasteEndpoints.Book.UpdateView
{
    internal sealed class Endpoint : Endpoint<BookUpdateViewRequest, BookUpdateViewResponse>
    {
        private readonly BookStoreEFCoreContext _context;

        public Endpoint(BookStoreEFCoreContext context) => _context = context;


        public override void Configure()
        {
            Get("MyLibrary/books/edit/{Id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(BookUpdateViewRequest r, CancellationToken c)
        {
            var bookToUpdate = await _context.Book.FirstOrDefaultAsync(b => b.Id == r.Id && !b.IsDeleted);

            if (bookToUpdate == null) { await SendNotFoundAsync(); return; }

            await SendAsync(new BookUpdateViewResponse() 
            {
                Id = bookToUpdate.Id,
                Title = bookToUpdate.Title,
                Isbn = bookToUpdate.Isbn,
                PublisherName = bookToUpdate.Publisher.FullName,
                Description = bookToUpdate.Description,
                IsAvailable = bookToUpdate.IsAvailaible
            });
        }
    }
}