using EFCorePr.FastEndpoints.Book.Delete;
using EFCorePr.Models;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EFCorePr.FasteEndpoints.Publisher.Delete
{
    internal sealed class Endpoint : Endpoint<PublisherDeleteRequest, PublisherDeleteResponse>
    {
        private readonly BookStoreEFCoreContext _context;

        public Endpoint(BookStoreEFCoreContext context) => _context = context;
        public override void Configure()
        {
            Delete("MyLibrary/publisher/delete/{Id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(PublisherDeleteRequest r, CancellationToken c)
        {
            var publisherToDelete = await _context.Publisher.FirstOrDefaultAsync(p => p.Id == r.Id && !p.IsDeleted);

            if (publisherToDelete == null) { await SendAsync(new PublisherDeleteResponse { Message = "Publisher Not Found!" }); return; }

            publisherToDelete.IsDeleted = true;
            await _context.SaveChangesAsync();

            await SendAsync(new PublisherDeleteResponse { Message = "Successfully Removed." });
        }
    }
}