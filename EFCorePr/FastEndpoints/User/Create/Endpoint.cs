using EFCorePr.FastEndpoints.Book.Create;
using EFCorePr.Models;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EFCorePr.FasteEndpoints.User.Create
{
    internal sealed class Endpoint : EndpointWithMapping<CreateUserViewModel, CreateUserResponse, Models.Customer>
    {
        private readonly BookStoreEFCoreContext _context;

        public Endpoint(BookStoreEFCoreContext context) => _context = context;
        public override void Configure()
        {
            Post("MyLibrary/user/add");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateUserViewModel r, CancellationToken c)
        {
            var userToCreate = MapToEntity(r);

            _context.Add(userToCreate);
            await _context.SaveChangesAsync();

            await SendOkAsync(new CreateUserResponse { Message = "Successfully Added!" });
        }

        public override Models.Customer MapToEntity(CreateUserViewModel r)
        {
            return new Models.Customer
            {
                FirstName = r.FirstName,
                LastName = r.LastName,
                NationalCode = r.NationalCode,
                PhoneNum = r.PhoneNum
            };
        }
    }
}