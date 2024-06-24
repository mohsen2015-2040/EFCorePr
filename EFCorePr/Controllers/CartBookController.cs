using EFCorePr.Controllers.Filter;
using EFCorePr.Models;
using EFCorePr.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCorePr.Controllers
{
    [TypeFilter(typeof(ExceptionHandler))]
    [ServiceFilter(typeof(LogActionActivity))]
    [Route("MyLibrary/[controller]")]
    public class CartBookController : Controller
    {
        private readonly BookStoreContext _dbContext;

        public CartBookController(BookStoreContext bookStoreContext)
        {
            _dbContext = bookStoreContext;
        }

        
        //    [HttpGet("Get-All")]
        //    public IActionResult Get()
        //    {
        //    }

        //    [HttpGet("search-rent/{Id}")]
        //    public async Task<IActionResult> GetByID(int Id)
        //    {
        //    }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(int bookID, int cartID)
        {

            var recordToAdd = _dbContext.CartBook.Add(new CartBook()
            {
                BookId = bookID,
                CartId = cartID
            });

            recordToAdd.Entity.Cart.TotalItem += 1;
            recordToAdd.Entity.Cart.TotalPrice += recordToAdd.Entity.Book.Price;

            await _dbContext.SaveChangesAsync();

            return Ok();
        }


        //    [HttpGet("edit/{Id}")]
        //    public async Task<IActionResult> Update(int Id)
        //    {
        //    }

        //    [HttpPost("Edit/{Id}")]
        //    public async Task<IActionResult> Update([FromForm] UpdateRentViewModel rentView )
        //    {
        //    }

        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var recordToRemove = await _dbContext.CartBook.FirstOrDefaultAsync(x => x.Id == Id);
            
            if(recordToRemove == null)
            {
                return BadRequest();
            }

            _dbContext.CartBook.Remove(recordToRemove);

            return Ok();
        }
    }
}
