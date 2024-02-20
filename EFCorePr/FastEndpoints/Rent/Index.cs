using EFCorePr.Models;
using FastEndpoints;

namespace EFCorePr.FastEndpoints.Rent
{
    public class Index : EndpointWithoutRequest
    {
        private readonly BookStoreEFCoreContext _context;

        public Index(BookStoreEFCoreContext context) => _context = context;

        public override void Configure()
        {
            Get("MyLibrary/Rent");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var rents = _context.Rent.Where(r => !r.IsDeleted).Select(r => new
            {
                Book = r.Book.Title,
                Reader = r.Customer.FirstName + " " + r.Customer.LastName,
                StartDate = r.StartDate,
                FinishDate = r.FinishDate
            }).ToList();

            await SendAsync(rents);
            
        }
    }
}
