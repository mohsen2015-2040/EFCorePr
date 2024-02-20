
using FastEndpoints;

namespace EFCorePr.FastEndpoints.Book.Delete
{
    internal sealed class DeleteBookRequest
    {
        public int Id { get; set; }
    }

    internal sealed class DeleteBookResponse
    {
        public string Message { get; set; } = string.Empty;
    }
}