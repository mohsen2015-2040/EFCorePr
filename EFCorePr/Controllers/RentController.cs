using EFCorePr.Controllers.Filter;
using EFCorePr.Models;
using EFCorePr.Services;
using EFCorePr.ViewModels.Rent;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCorePr.Controllers
{
    [TypeFilter(typeof(ExceptionHandler))]
    [Route("MyLibrary/[controller]")]
    public class RentController : Controller
    {
        private readonly BookStoreEFCoreContext _dbContext;
        private readonly IGenerateGuideToRoutsService _generateGuide;
        
        public RentController(BookStoreEFCoreContext bookStoreContext, IGenerateGuideToRoutsService generateGuide, ILogger<RentController> logger)
        {
            _dbContext = bookStoreContext;
            _generateGuide = generateGuide;
        }

        public ActionResult Index()
        {
            var guideMessage = _generateGuide.GenerateMessage(typeof(Rent));
            return Ok(guideMessage);
        }

        //Actions
        [ServiceFilter(typeof(LogActionActivity))]
        [HttpGet("Get-All")]
        public IActionResult Get()
        {
            var rents = _dbContext.Rent.Where(r => !r.IsDeleted).Select(r => new
            {
                BookTitle = r.Book.Title,
                BookIsbn = r.Book.Isbn,
                CustomerName = (r.Customer.FirstName + r.Customer.LastName),
                CustomerNationalCode = r.Customer.NationalCode,
                CustomerPhoneNum = r.Customer.PhoneNum,
                StartDate = r.StartDate,
                EndDate = r.FinishDate
            });

            /*List<RentViewData> rentViews = new List<RentViewData>();
            foreach(var rent in rents)
            {
                rentViews.Add(new RentViewData
                {
                    BookTitle = rent.Book.Title,
                    BookIsbn = rent.Book.Isbn,
                    CustomerName = (rent.Customer.FirstName + rent.Customer.LastName),
                    CustomerNationalCode = rent.Customer.NationalCode,
                    StartDate = rent.StartDate,
                    EndDate = rent.FinishDate,
                    CustomerPhoneNum = rent.Customer.PhoneNum
                });
            }*/

            return Ok(rents);
        }

        [ServiceFilter(typeof(LogActionActivity))]
        [HttpGet("search-rent/{Id}")]
        public async Task<IActionResult> GetByID(int Id)
        {
            var selectedRent = await _dbContext.Rent.FirstOrDefaultAsync(x => x.Id == Id && !x.IsDeleted);

            if (selectedRent == null)
                return NotFound("Item Not Found!");

            return Ok(new
            {
                BookTitle = selectedRent.Book.Title,
                CustomerName = (selectedRent.Customer.FirstName + selectedRent.Customer.LastName),
                CustomerNationalCode = selectedRent.Customer.NationalCode,
                BookIsbn = selectedRent.Book.Isbn,
                StartDate = selectedRent.StartDate,
                EndDate = selectedRent.FinishDate,
                CustomerPhoneNum = selectedRent.Customer.PhoneNum
            });
        }

        [ServiceFilter(typeof(LogActionActivity))]
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromForm][Bind("BookIsbn", "CustomerNationalCode", "EndDate")] AddRentViewModel addRentView)
        {
            var rentViewBook = await _dbContext.Book.FirstOrDefaultAsync(b => b.Isbn == addRentView.BookIsbn && !b.IsDeleted);
            var rentViewCustomer = await _dbContext.Customer.FirstOrDefaultAsync(c => c.NationalCode == addRentView.CustomerNationalCode && !c.IsDeleted);

            if (rentViewBook == null || rentViewCustomer == null)
                return NotFound("Invalid Input!");

            if (ModelState.IsValid)
            {
                _dbContext.Rent.Add(new Rent
                {
                    BookId = rentViewBook.Id,
                    CustomerId = rentViewCustomer.Id,
                    FinishDate = addRentView.EndDate
                });
                rentViewBook.IsAvailaible = false;

                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            return Ok(addRentView);

        }


        [HttpGet("edit/{Id}")]
        public async Task<IActionResult> Update(int Id)
        {
            var selectedRent = await _dbContext.Rent.FirstOrDefaultAsync(r => r.Id == Id && !r.IsDeleted);

            if (selectedRent == null)
                return NotFound("The Item Not Found!");

            return Ok(new RentViewData 
            { 
                BookIsbn = selectedRent.Book.Isbn,
                CustomerNationalCode = selectedRent.Customer.NationalCode,
                StartDate = selectedRent.StartDate,
                EndDate = selectedRent.FinishDate
            });
        }

        [ServiceFilter(typeof(LogActionActivity))]
        [HttpPost("Edit/{Id}")]
        public async Task<IActionResult> Update(int Id, [FromForm][Bind("BookIsbn", "CustomerNationalCode", "StartDate", "FinishDate")] RentViewData rentView )
        {
            var selectedRent = await _dbContext.Rent.FirstOrDefaultAsync(x => x.Id == Id && !x.IsDeleted);
            var rentViewBook = await _dbContext.Book.FirstOrDefaultAsync(b => b.Isbn == rentView.BookIsbn && !b.IsDeleted);
            var rentViewCustomer = await _dbContext.Customer.FirstOrDefaultAsync(c => c.NationalCode == rentView.CustomerNationalCode && !c.IsDeleted);

            if (selectedRent == null || rentViewBook == null || rentViewCustomer == null)
                return NotFound("Invalid Input!");

            if (ModelState.IsValid)
            {
                selectedRent.StartDate = rentView.StartDate;
                selectedRent.FinishDate = rentView.EndDate;
                selectedRent.CustomerId = rentViewCustomer.Id;
                selectedRent.BookId = rentViewBook.Id;

                await _dbContext.SaveChangesAsync();

                return Ok();
            }

            return Ok(rentView);
        }

        [ServiceFilter(typeof(LogActionActivity))]
        [HttpPost("Delete/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var selectedRent = await _dbContext.Rent.FirstOrDefaultAsync(x => x.Id == Id && !x.IsDeleted);

            if (selectedRent == null)
                return NotFound("The Item not found!");
            
            selectedRent.IsDeleted = true;
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
