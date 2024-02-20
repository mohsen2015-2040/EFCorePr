using EFCorePr.Models;
using FastEndpoints;

namespace EFCorePr.FasteEndpoints.Publisher.Create
{
    internal sealed class Endpoint : EndpointWithMapping<CreatePublisherViewModel, CreatePublisherResponse, Models.Publisher>
    {
        private readonly BookStoreEFCoreContext _context;

        public Endpoint(BookStoreEFCoreContext context) => _context = context;


        public override void Configure()
        {
            Post("MyLibrary/publisher/add");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreatePublisherViewModel r, CancellationToken c)
        {
            if (!ValidationFailed)
            {
                var publisherToCreate = MapToEntity(r);

                _context.Add(publisherToCreate);
                await _context.SaveChangesAsync();

                await SendAsync(new CreatePublisherResponse { Message = "Successfully Added." });
                return;
            }
            await SendOkAsync(new CreatePublisherResponse { Message = "Invalid Input!" });
        }


        public override Models.Publisher MapToEntity(CreatePublisherViewModel r)
        {
            return new Models.Publisher { FullName = r.FullName };
        }
    }
}