using EFCorePr.Models;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EFCorePr.FasteEndpoints.User.Delete
{
    internal sealed class Endpoint : Endpoint<UserDeleteRequest, UserDeleteResponse>
    {
        private readonly BookStoreEFCoreContext _context;

        public Endpoint(BookStoreEFCoreContext context) => _context = context;

        public override void Configure()
        {
            Delete("MyLibrary/user/delete/{Id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(UserDeleteRequest r, CancellationToken c)
        {
            var userToDelete = await _context.Customer.FirstOrDefaultAsync(u => u.Id == r.Id && !u.IsDeleted);

            if (userToDelete == null) { await SendAsync(new UserDeleteResponse { Message = "Not Found!" }); return; }

            userToDelete.IsDeleted = true;

            await _context.SaveChangesAsync();

            await SendOkAsync(new UserDeleteResponse { Message = "Successfully Removed." });
        }
    }
}