using EFCorePr.Models;

namespace EFCorePr.ViewModels
{
    public class RentViewData
    {
        public string BookTitle { get; set; } = string.Empty;
        public string BookIsbn { get; set; } = string.Empty;
        public string CustomerNationalCode { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhoneNum { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public RentViewData() { }
        public RentViewData(Rent dbRent)
        {
            BookTitle = dbRent.Book.Title;
            CustomerName = dbRent.Customer.FirstName + dbRent.Customer.LastName;
            CustomerPhoneNum = dbRent.Customer.PhoneNum;
            StartDate = dbRent.StartDate;
            EndDate = dbRent.FinishDate;
        }

    }
}
