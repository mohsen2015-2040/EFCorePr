using EFCorePr.Models;
using FastEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using FromBbody = FastEndpoints.FromBodyAttribute;

namespace EFCorePr.FastEndpoints.Book.Create
{
    internal sealed class Endpoint : EndpointWithMapping<CreateBookViewModel, CreateBookResponse, Models.Book>
    {
        private readonly BookStoreEFCoreContext _context;

        public Endpoint(BookStoreEFCoreContext context) => _context = context;


        public override void Configure()
        {
            Post("MyLibrary/books/add");
            AllowAnonymous();
        }

        public override async Task HandleAsync([Microsoft.AspNetCore.Mvc.FromBody] CreateBookViewModel r, CancellationToken c)
        {
            var bookPublisher = await _context.Publisher.FirstOrDefaultAsync(p => p.FullName == r.PublisherName && !p.IsDeleted);

            if (bookPublisher == null)
            {
                bookPublisher = new Models.Publisher { FullName = r.PublisherName };

                _context.Publisher.Add(bookPublisher);
                await _context.SaveChangesAsync();
            }

            var bookToCreate = MapToEntity(r);

            bookToCreate.PublisherId = bookPublisher.Id;


            _context.Add(bookToCreate);
            await _context.SaveChangesAsync();

            await SendAsync(new CreateBookResponse { Message = "Successfully Added!" });
        }

        public override Models.Book MapToEntity(CreateBookViewModel r)
        {
            return new Models.Book
            {
                Title = r.Title,
                Isbn = r.Isbn,
                Description = r.Description
            };
        }
    }
}