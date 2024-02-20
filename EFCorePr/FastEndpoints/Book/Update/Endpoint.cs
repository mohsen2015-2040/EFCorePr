using EFCorePr.Models;
using FastEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCorePr.FasteEndpoints.Book.Update
{
    internal sealed class Endpoint : Endpoint<UpdateBookViewModel, UpdateBookResponse>
    {
        private readonly BookStoreEFCoreContext _context;

        public Endpoint(BookStoreEFCoreContext context) => _context = context;


        public override void Configure()
        {
            Post("MyLibrary/books/edit/{Id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync([Microsoft.AspNetCore.Mvc.FromBody]UpdateBookViewModel r, CancellationToken c)
        {
            if (!ValidationFailed)
            {
                var bookToUpdate = await _context.Book.FirstOrDefaultAsync(b => b.Id == r.Id && !b.IsDeleted);

                if (bookToUpdate == null)
                {
                    await SendOkAsync(new UpdateBookResponse { Message = "Invalid Input!" });
                    return;
                }

                var bookPublisher = await _context
                    .Publisher
                    .FirstOrDefaultAsync(p => p.FullName == r.PublisherName && !p.IsDeleted);

                if (bookPublisher == null)
                {
                    bookPublisher = new Models.Publisher { FullName = r.PublisherName };
                    _context.Publisher.Add(bookPublisher);
                    await _context.SaveChangesAsync();
                }

                
                bookToUpdate.Title = r.Title;
                bookToUpdate.Description = r.Description;
                bookToUpdate.Isbn = r.Isbn;
                bookToUpdate.PublisherId = bookPublisher.Id;

                await _context.SaveChangesAsync();

                await SendOkAsync(new UpdateBookResponse { Message = "Successfully Updated!" });
                return;
            }

            await SendAsync(new UpdateBookResponse { Message = "Invalid Input!"});
        }
    }
}