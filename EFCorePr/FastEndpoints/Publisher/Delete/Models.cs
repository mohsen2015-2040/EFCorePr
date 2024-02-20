using FastEndpoints;

namespace EFCorePr.FasteEndpoints.Publisher.Delete
{
    internal sealed class PublisherDeleteRequest
    {
        public int Id { get; set; }
    }

    internal sealed class PublisherDeleteResponse
    {
        public string Message { get; set; } = string.Empty;
    }
}