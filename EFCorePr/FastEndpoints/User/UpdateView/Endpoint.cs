using EFCorePr.Models;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EFCorePr.FasteEndpoints.User.UpdateView
{
    internal sealed class Endpoint : Endpoint<UpdateViewRequest, UpdateViewResponse>
    {
        private readonly BookStoreEFCoreContext _context;
        public Endpoint(BookStoreEFCoreContext context) => _context = context;

        public override void Configure()
        {
            Get("MyLibrary/user/edit/{Id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(UpdateViewRequest r, CancellationToken c)
        {
            var userToUpdate = await _context.Customer.FirstOrDefaultAsync(c => c.Id == r.Id && !c.IsDeleted);

            if (userToUpdate == null) { await SendNotFoundAsync(); return; }

            await SendOkAsync(new UpdateViewResponse() 
            {
                Id = userToUpdate.Id,
                FirstName = userToUpdate.FirstName,
                LastName = userToUpdate.LastName,
                NationalCode = userToUpdate.NationalCode,
                PhoneNum = userToUpdate.PhoneNum
            });
        }
    }
}