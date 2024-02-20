using EFCorePr.Models;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EFCorePr.FasteEndpoints.Publisher.Update
{
    internal sealed class Endpoint : Endpoint<UpdatePublisherViewModel, UpdatePublisherResponse>
    {
        private readonly BookStoreEFCoreContext _context;

        public Endpoint(BookStoreEFCoreContext context) => _context = context;


        public override void Configure()
        {
            Post("MyLibrary/publisher/edit/{Id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(UpdatePublisherViewModel r, CancellationToken c)
        {
            var publisherToUpdate = await _context.Publisher.FirstOrDefaultAsync(p => p.Id == r.Id && !p.IsDeleted);

            if (publisherToUpdate == null)
            {
                await SendOkAsync(new UpdatePublisherResponse { Message = "Invalid Input!" });
                return;
            }

            publisherToUpdate.FullName = r.FullName;
            await _context.SaveChangesAsync();

            await SendOkAsync(new UpdatePublisherResponse { Message = "Successfully Updated." });
        }
    }
}