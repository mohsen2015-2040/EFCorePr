using FastEndpoints;

namespace EFCorePr.FasteEndpoints.Rent.UpdateView
{
    internal sealed class RentUpdateViewRequest
    {
        public int Id { get; set; }
    }

    internal sealed class RentUpdateViewResponse
    {
        public int Id { get; set; }
        public string BookIsbn { get; set; } = string.Empty;
        public string CustomerNationalCode { get; set; } = string.Empty;
        public DateTime EndDate { get; set; }
    }
}