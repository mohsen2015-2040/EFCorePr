namespace EFCorePr.ViewModels.Customer
{

    public class UpdateCustomerViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string NationalCode { get; set; } = string.Empty;
        public string PhoneNum { get; set; } = string.Empty;
    }
}
