using EFCorePr.Controllers.Filter;
using EFCorePr.Models;
using EFCorePr.ViewModels;
using EFCorePr.ViewModels.Rent.Create;
using EFCorePr.ViewModels.Rent.Update;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCorePr.Controllers
{
    [TypeFilter(typeof(ExceptionHandler))]
    [ServiceFilter(typeof(LogActionActivity))]
    [Route("MyLibrary/[controller]")]
    public class RentController : Controller
    {
        private readonly BookStoreEFCoreContext _dbContext;
        
        public RentController(BookStoreEFCoreContext bookStoreContext)
        {
            _dbContext = bookStoreContext;
        }

        // ******Actions******
        [HttpGet("Get-All")]
        public IActionResult Get()
        {
            var rents = _dbContext.Rent.Where(r => !r.IsDeleted).ToList()
                .Select(r => new
                {
                    BookTitle = r.Book.Title,
                    BookIsbn = r.Book.Isbn,
                    CustomerName = (r.Customer.FirstName + r.Customer.LastName),
                    CustomerNationalCode = r.Customer.NationalCode,
                    StartDate = r.StartDate,
                    EndDate = r.FinishDate,
                    CustomerPhoneNum = r.Customer.PhoneNum
                }).ToList();

            return Ok(rents);
        }

        [HttpGet("search-rent/{Id}")]
        public async Task<IActionResult> GetByID(int Id)
        {
            var selectedRent = await _dbContext.Rent
                .FirstOrDefaultAsync(x => x.Id == Id && !x.IsDeleted);

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

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromForm]CreateRentViewModel rentView)
        {
            var rentViewBook = await _dbContext.Book
                .FirstOrDefaultAsync(b => b.Isbn == rentView.BookIsbn && !b.IsDeleted);

            var rentViewCustomer = await _dbContext.Customer
                .FirstOrDefaultAsync(c => c.NationalCode == rentView.CustomerNationalCode && !c.IsDeleted);

            if (rentViewBook == null || rentViewCustomer == null)
                return NotFound("Invalid Input!");

            if (ModelState.IsValid)
            {
                _dbContext.Rent.Add(new Rent
                {
                    BookId = rentViewBook.Id,
                    CustomerId = rentViewCustomer.Id,
                    FinishDate = rentView.EndDate
                });
                rentViewBook.IsAvailaible = false;

                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            return Ok(rentView);

        }


        [HttpGet("edit/{Id}")]
        public async Task<IActionResult> Update(int Id)
        {
            var selectedRent = await _dbContext.Rent.FirstOrDefaultAsync(r => r.Id == Id && !r.IsDeleted);

            if (selectedRent == null)
                return NotFound("The Item Not Found!");

            return Ok(new UpdateRentViewModel 
            { 
                BookIsbn = selectedRent.Book.Isbn,
                CustomerNationalCode = selectedRent.Customer.NationalCode,
                EndDate = selectedRent.FinishDate
            });
        }

        [HttpPost("Edit/{Id}")]
        public async Task<IActionResult> Update([FromForm] UpdateRentViewModel rentView )
        {
            var selectedRent = await _dbContext.Rent
                .FirstOrDefaultAsync(x => x.Id == rentView.Id && !x.IsDeleted);
            
            var rentViewBook = await _dbContext.Book
                .FirstOrDefaultAsync(b => b.Isbn == rentView.BookIsbn && !b.IsDeleted);
            
            var rentViewCustomer = await _dbContext.Customer.FirstOrDefaultAsync(c => c.NationalCode == rentView.CustomerNationalCode && !c.IsDeleted);


            if (selectedRent == null || rentViewBook == null || rentViewCustomer == null)
                return NotFound("Invalid Input!");

            if (ModelState.IsValid)
            {
                selectedRent.FinishDate = rentView.EndDate;
                selectedRent.CustomerId = rentViewCustomer.Id;
                selectedRent.BookId = rentViewBook.Id;

                await _dbContext.SaveChangesAsync();

                return Ok();
            }

            return Ok(rentView);
        }

        [HttpPost("Delete/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var selectedRent = await _dbContext.Rent
                .FirstOrDefaultAsync(x => x.Id == Id && !x.IsDeleted);

            if (selectedRent == null)
                return NotFound("The Item not found!");
            
            selectedRent.IsDeleted = true;
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
