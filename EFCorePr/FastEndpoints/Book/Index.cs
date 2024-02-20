using EFCorePr.FasteEndpoints.Book.Update;
using EFCorePr.Models;
using FastEndpoints;
using FastEndpoints.Security;

namespace EFCorePr.FastEndpoints.Book
{
    public class Index : EndpointWithoutRequest
    {
        private readonly BookStoreEFCoreContext _context;

        public Index(BookStoreEFCoreContext context) => _context = context;

        public override void Configure()
        {
            Get("MyLibrary/Books");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var books = _context.Book.Where(b => !b.IsDeleted).Select(b => new
            {
                Title = b.Title,
                Isbn = b.Isbn,
                Description = b.Description,
                PublisherName = b.Publisher.FullName,
                IsAvailable = b.IsAvailaible
            }).ToList();

            await SendAsync(books);
        }
    }
}
