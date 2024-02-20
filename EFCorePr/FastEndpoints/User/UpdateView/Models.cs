using FastEndpoints;

namespace EFCorePr.FasteEndpoints.User.UpdateView
{
    internal sealed class UpdateViewRequest
    {
        public int Id { get; set; }
    }
    internal sealed class UpdateViewResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string NationalCode { get; set; } = string.Empty;

        public string PhoneNum { get; set; } = string.Empty;
    }
}