using FastEndpoints;

namespace EFCorePr.FastEndpoints.MyLibrary
{
    public class Index : EndpointWithoutRequest
    {
        public override void Configure()
        {
            Get("MyLibrary");
            AllowAnonymous();
        }
        public override async Task HandleAsync(CancellationToken ct)
        {
            await SendOkAsync("Welcome!");
        }
    }
}
