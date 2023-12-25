namespace EFCorePr.ViewModels.Rent
{
    public class UpdateRentViewModel
    {
        public int Id { get; set; }
        public string BookIsbn { get; set; } = string.Empty;
        public string CustomerNationalCode { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
