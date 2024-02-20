using EFCorePr.Models;
using FastEndpoints;

namespace EFCorePr.FastEndpoints.User
{
    public class Index : EndpointWithoutRequest
    {
        private readonly BookStoreEFCoreContext _context;

        public Index(BookStoreEFCoreContext context) => _context = context;

        public override void Configure()
        {
            Get("MyLibrary/User");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var users = _context.Customer.Where(c => !c.IsDeleted).Select(c => new
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                NationalCode = c.NationalCode,
                PoneNum = c.PhoneNum
            }).ToList();
            
            await SendOkAsync(users);
        }
    }
}
