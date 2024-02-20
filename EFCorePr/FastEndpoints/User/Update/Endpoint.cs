using EFCorePr.Models;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EFCorePr.FasteEndpoints.User.Update
{
    internal sealed class Endpoint : Endpoint<UserUpdateViewModel, UserUpdateResponse>
    {
        private readonly BookStoreEFCoreContext _context;
        public Endpoint(BookStoreEFCoreContext context) => _context = context;

        public override void Configure()
        {
            Post("MyLibrary/User/edit/{Id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(UserUpdateViewModel r, CancellationToken c)
        {
            var userToUpdate = await _context.Customer
                .FirstOrDefaultAsync(u => u.Id == r.Id && !u.IsDeleted);

            if (userToUpdate == null) { await SendAsync(new UserUpdateResponse { Message = "Invalid Input!" }); return; }

            userToUpdate.FirstName = r.FirstName;
            userToUpdate.LastName = r.LastName;
            userToUpdate.PhoneNum = r.PhoneNum;
            userToUpdate.NationalCode = r.NationalCode;
            await _context.SaveChangesAsync();

            await SendOkAsync(new UserUpdateResponse { Message = "Successfully Added." });
        }
    }
}