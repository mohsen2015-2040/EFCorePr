using EFCorePr.Models;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EFCorePr.FasteEndpoints.Rent.UpdateView
{
    internal sealed class Endpoint : Endpoint<RentUpdateViewRequest, RentUpdateViewResponse>
    {
        private readonly BookStoreEFCoreContext _context;
        public Endpoint(BookStoreEFCoreContext context) => _context = context;

        public override void Configure()
        {
            Get("MyLibrary/rent/edit/{Id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(RentUpdateViewRequest r, CancellationToken c)
        {
            var rentToUpdate = await _context.Rent.FirstOrDefaultAsync(x => x.Id == r.Id && !x.IsDeleted);

            if (rentToUpdate == null) { await SendNotFoundAsync(); return; }

            await SendOkAsync(new RentUpdateViewResponse 
            {
                Id = rentToUpdate.Id,
                BookIsbn = rentToUpdate.Book.Isbn,
                CustomerNationalCode = rentToUpdate.Customer.NationalCode,
                EndDate = rentToUpdate.FinishDate
            });
        }
    }
}