namespace EFCorePr.ViewModels.Customer.Create
{
    public class CreateCustomerViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNum { get; set; } = string.Empty;
        public string NationalCode { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
