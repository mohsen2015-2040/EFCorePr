using EFCorePr.Models;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EFCorePr.FasteEndpoints.Rent.Update
{
    internal sealed class Endpoint : Endpoint<UpdateRentViewModel, UpdateRentResponse>
    {
        private readonly BookStoreEFCoreContext _context;

        public Endpoint(BookStoreEFCoreContext context) => _context = context;

        public override void Configure()
        {
            Post("MyLibrary/Rent/edit/{Id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(UpdateRentViewModel r, CancellationToken c)
        {
            if (!ValidationFailed)
            {
                var rentBook = await _context.Book
                    .FirstOrDefaultAsync(b => b.Isbn == r.BookIsbn  && !b.IsDeleted);

                if (rentBook == null) { await SendAsync(new UpdateRentResponse { Message = "Invalid Book!" }); return; }

                var rentCustomer = await _context.Customer
                    .FirstOrDefaultAsync(c => c.NationalCode == r.CustomerNationalCode && !c.IsDeleted);

                if (rentCustomer == null) { await SendAsync(new UpdateRentResponse { Message = "Invalid User!" }); return; }


                var rentToUpdate = await _context.Rent.FirstOrDefaultAsync(x => x.Id == r.Id && !x.IsDeleted);

                if (rentToUpdate == null) { await SendAsync(new UpdateRentResponse { Message = "Invalid Record!" }); return; }

                rentToUpdate.CustomerId = rentCustomer.Id;
                rentToUpdate.BookId = rentBook.Id;
                rentToUpdate.FinishDate = r.EndDate;

                await _context.SaveChangesAsync();

                await SendAsync(new UpdateRentResponse { Message = "Successfully Edited." });
                return;
            }
            await SendAsync(new UpdateRentResponse { Message = "Invalid Input!" });
        }
    }
}