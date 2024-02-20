using EFCorePr.Models;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EFCorePr.FasteEndpoints.Rent.Create
{
    internal sealed class Endpoint : EndpointWithMapping<CreateRentViewModel, CreateRentResponse, Models.Rent>
    {
        private readonly BookStoreEFCoreContext _context;

        public Endpoint(BookStoreEFCoreContext context) => _context = context;

        public override void Configure()
        {
            Post("MyLibrary/Rent/add");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateRentViewModel r, CancellationToken c)
        {
            if (!ValidationFailed)
            {
                var rentBook = await _context.Book
                    .FirstOrDefaultAsync(b => b.Isbn == r.BookIsbn && !b.IsDeleted && b.IsAvailaible);

                if (rentBook == null) { await SendAsync(new CreateRentResponse { Message = "Invalid Book!" }); return; }

                var rentCustomer = await _context.Customer
                    .FirstOrDefaultAsync(c => c.NationalCode == r.CustomerNationalCode && !c.IsDeleted);

                if (rentCustomer == null) { await SendAsync(new CreateRentResponse { Message = "Invalid User!" }); return; }

                var rentToCreate = MapToEntity(r);

                rentToCreate.CustomerId = rentCustomer.Id;
                rentToCreate.BookId = rentBook.Id;

                _context.Add(rentToCreate);
                rentBook.IsAvailaible = false;
                await _context.SaveChangesAsync();

                await SendAsync(new CreateRentResponse { Message = "Successfully Added." });
                

                return;
            }

            await SendAsync(new CreateRentResponse { Message = "Invalid Input!" });
        }

        public override Models.Rent MapToEntity(CreateRentViewModel r)
        {
            return new Models.Rent
            {
                FinishDate = DateTime.Now.AddDays(7)
            };
        }
    }
}