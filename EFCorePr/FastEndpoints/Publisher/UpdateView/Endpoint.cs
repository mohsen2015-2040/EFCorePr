using EFCorePr.Models;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EFCorePr.FasteEndpoints.Publisher.UpdateView
{
    internal sealed class Endpoint : Endpoint<PublisherUpdateVeiewRequest, PublisherUpdateViewResponse>
    {
        private readonly BookStoreEFCoreContext _context;
        public Endpoint(BookStoreEFCoreContext context) => _context = context;

        public override void Configure()
        {
            Get("MyLibrary/publisher/edit/{Id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(PublisherUpdateVeiewRequest r, CancellationToken c)
        {
            var publisherToUpdate = await _context.Publisher.FirstOrDefaultAsync(p => p.Id == r.Id && !p.IsDeleted);
            
            if(publisherToUpdate == null) { await SendNotFoundAsync(); return; }
            
            await SendAsync(new PublisherUpdateViewResponse() { Id = publisherToUpdate.Id});
        }
    }
}