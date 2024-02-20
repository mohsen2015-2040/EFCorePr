using EFCorePr.Models;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EFCorePr.FastEndpoints.Publisher.GetById
{
    internal sealed class Endpoint :  Endpoint<PublisherGetByIdRequest>
    {
        private readonly BookStoreEFCoreContext _context;

        public Endpoint(BookStoreEFCoreContext context) => _context = context;

        public override void Configure()
        {
            Get("MyLibrary/publisher/search/{Id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(PublisherGetByIdRequest r, CancellationToken c)
        {
            var selectedPublisher = await _context.Publisher.FirstOrDefaultAsync(p => p.Id == r.Id && !p.IsDeleted);

            if (selectedPublisher == null) { await SendNotFoundAsync(); return; }
            
            await SendOkAsync(new
            {
                FullName = selectedPublisher.FullName
            });
        }
    }
}