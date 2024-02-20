using EFCorePr.Models;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EFCorePr.FastEndpoints.Rent.GetById
{
    internal sealed class Endpoint :  Endpoint<RentGetByIdRequest>
    {
        private readonly BookStoreEFCoreContext _context;

        public Endpoint(BookStoreEFCoreContext context) => _context = context;

        public override void Configure()
        {
            Get("MyLibrary/rent/search/{Id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(RentGetByIdRequest r, CancellationToken c)
        {
            var selectedRent = await _context.Rent.FirstOrDefaultAsync(x => x.Id == r.Id && !x.IsDeleted);

            if (selectedRent == null) { await SendNotFoundAsync(); return; }
            
            await SendOkAsync(new
            {
                Book = selectedRent.Book.Title,
                Reader = selectedRent.Customer.FirstName + " " + selectedRent.Customer.LastName,
                StartDate = selectedRent.StartDate,
                FinishDate = selectedRent.FinishDate
            });
        }
    }
}