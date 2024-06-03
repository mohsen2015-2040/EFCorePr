namespace EFCorePr.ViewModels.Rent.Create
{
    public class CreateRentViewModel
    {
        public int Id { get; set; }     
        public string BookIsbn { get; set; } = string.Empty;
        public string CustomerNationalCode { get; set; } = string.Empty;
        public DateTime EndDate { get; set; }
    }
}
