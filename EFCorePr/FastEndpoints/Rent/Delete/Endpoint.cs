using EFCorePr.FasteEndpoints.Publisher.Delete;
using EFCorePr.Models;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EFCorePr.FasteEndpoints.Rent.Delete
{
    internal sealed class Endpoint : Endpoint<RentDeleteRequest, RentDeleteResponse>
    {
        private readonly BookStoreEFCoreContext _context;

        public Endpoint(BookStoreEFCoreContext context) => _context = context;
        public override void Configure()
        {
            Delete("MyLibrary/rent/delete/{Id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(RentDeleteRequest r, CancellationToken c)
        {
            var rentToDelete = await _context.Rent.FirstOrDefaultAsync(x => x.Id == r.Id && !x.IsDeleted);

            if (rentToDelete == null) { await SendAsync(new RentDeleteResponse { Message = "Not Found!" }); return; }

            rentToDelete.IsDeleted = true;
            rentToDelete.Book.IsAvailaible = true;

            await _context.SaveChangesAsync();

            await SendAsync(new RentDeleteResponse { Message = "Successfully Removed." });
        }
    }
}