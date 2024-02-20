using FastEndpoints;

namespace EFCorePr.FasteEndpoints.Publisher.UpdateView
{
    internal sealed class PublisherUpdateVeiewRequest
    {
        public int Id { get; set; }
    }

    internal sealed class PublisherUpdateViewResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
    }
}