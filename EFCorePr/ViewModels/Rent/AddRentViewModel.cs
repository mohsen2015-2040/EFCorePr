namespace EFCorePr.ViewModels.Rent
{
    public class AddRentViewModel
    {
        public string BookIsbn { get; set; } = string.Empty;
        public string CustomerNationalCode { get; set; } = string.Empty;
        public DateTime EndDate { get; set; }

    }
}
