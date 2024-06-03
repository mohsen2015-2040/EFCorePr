using Microsoft.AspNetCore.Mvc;

namespace EFCorePr.ViewModels.Rent.Update
{
    public class UpdateRentViewModel
    {
        [FromRoute]
        public int Id { get; set; }
        public string BookIsbn { get; set; } = string.Empty;
        public string CustomerNationalCode { get; set; } = string.Empty;
        public DateTime EndDate { get; set; }
    }
}
