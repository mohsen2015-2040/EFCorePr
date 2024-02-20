using FastEndpoints;

namespace EFCorePr.FasteEndpoints.Rent.Delete
{
    internal sealed class RentDeleteRequest
    {
        public int Id { get; set; }
    }

    internal sealed class RentDeleteResponse
    {
        public string Message { get; set; } = string.Empty;
    }
}