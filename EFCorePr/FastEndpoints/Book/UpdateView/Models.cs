using FastEndpoints;

namespace EFCorePr.FasteEndpoints.Book.UpdateView
{
    internal sealed class BookUpdateViewRequest
    {
        public int Id { get; set; }
    }

    internal sealed class BookUpdateViewResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Isbn { get; set; } = string.Empty;
        public string PublisherName { get; set; } = string.Empty;
        public bool IsAvailable{ get; set;}
    }
}