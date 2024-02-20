using EFCorePr.Models;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EFCorePr.FastEndpoints.User.GetById
{
    internal sealed class Endpoint :  Endpoint<UserGetByIdRequest>
    {
        private readonly BookStoreEFCoreContext _context;

        public Endpoint(BookStoreEFCoreContext context) => _context = context;

        public override void Configure()
        {
            Get("MyLibrary/user/search/{Id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(UserGetByIdRequest r, CancellationToken c)
        {
            var selectedUser = await _context.Customer.FirstOrDefaultAsync(u => u.Id == r.Id && !u.IsDeleted);

            if (selectedUser == null) { await SendNotFoundAsync(); return; }
            
            await SendOkAsync(new
            {
                FirstName = selectedUser.FirstName,
                LastName = selectedUser.LastName,
                NationalCode = selectedUser.NationalCode,
                PoneNum = selectedUser.PhoneNum
            });
        }
    }
}