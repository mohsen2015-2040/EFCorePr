using Microsoft.AspNetCore.Mvc;

namespace EFCorePr.ViewModels.Customer.Update
{
    public class UpdateCustomerViewModel
    {
        [FromRoute]
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNum { get; set; } = string.Empty;
        
        //public string Password { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
