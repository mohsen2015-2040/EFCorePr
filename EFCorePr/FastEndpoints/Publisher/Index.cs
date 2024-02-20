using EFCorePr.Models;
using FastEndpoints;

namespace EFCorePr.FastEndpoints.Publisher
{
    public class Index : EndpointWithoutRequest
    {
        private readonly BookStoreEFCoreContext _context;

        public Index(BookStoreEFCoreContext context) => _context = context;

        public override void Configure()
        {
            Get("MyLibrary/publisher");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var publishers = _context.Publisher.Where(p => !p.IsDeleted).Select(p => new
            {
                Title = p.FullName
            }).ToList();

            await SendOkAsync(publishers);
        }
    }
}
