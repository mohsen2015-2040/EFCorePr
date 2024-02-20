using FastEndpoints;

namespace EFCorePr.FasteEndpoints.User.Delete
{
    internal sealed class UserDeleteRequest
    {
        public int Id { get; set; } 
    }

    internal sealed class UserDeleteResponse
    {
        public string Message { get; set; } = string.Empty;
    }
}