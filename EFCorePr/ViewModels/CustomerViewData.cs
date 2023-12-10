using EFCorePr.Models;

namespace EFCorePr.ViewModels
{
    public class CustomerViewData
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNum { get; set; } = string.Empty;
        public string NationalCode { get; set; } = string.Empty;

        public CustomerViewData() { }
        public CustomerViewData(Customer dbCustomer)
        {
            FirstName = dbCustomer.FirstName;
            LastName = dbCustomer.LastName;
            PhoneNum = dbCustomer.PhoneNum;
        }
    }
}
