using EFCorePr.Controllers.Filter;
using EFCorePr.Models;
using EFCorePr.Services;
using Microsoft.AspNetCore.Mvc;

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

        [ServiceFilter(typeof(LogActionActivity))]
        [HttpGet("Get-All")]
        public IActionResult Get()
        {
            var rents = from r in _dbContext.Rent
                             where (r.IsDeleted != true)
                             select r;

            return Ok(rents.ToArray());
        }

        [ServiceFilter(typeof(LogActionActivity))]
        [HttpGet("search-rent")]
        public IActionResult GetByID(int Id)
        {
            var selectedRent = _dbContext.Rent.FirstOrDefault(x => x.Id == Id && x.IsDeleted != true);

            if (selectedRent == null)
                return NotFound("Item Not Found!");
            else
                return Ok(selectedRent);
        }

        [ServiceFilter(typeof(LogActionActivity))]
        [HttpPost("Add")]
        public async Task<IActionResult> Add(Rent rent)
        {
            _dbContext.Rent.Add(rent);
            await _dbContext.SaveChangesAsync();

            return Ok("Successfully added.");

        }

        [ServiceFilter(typeof(LogActionActivity))]
        [HttpPost("Edit")]
        public async Task<IActionResult> Update(int Id, [Bind("Id", "StartDate", "FinishDate")] Rent rent)
        {
            var selectedRent = _dbContext.Rent.FirstOrDefault(x => x.Id == Id && x.IsDeleted != true);

            if (selectedRent == null)
                return NotFound("Item not found!");
            else if (Id != rent.Id)
                return BadRequest("The provided Id doesn't match Id in rents data!");
            else if (ModelState.IsValid)
            {
                selectedRent.StartDate = rent.StartDate;
                selectedRent.FinishDate = rent.FinishDate;
                await _dbContext.SaveChangesAsync();

                return Ok("Successfully Updated");
            }
            else
                return Ok(rent);
        }

        [ServiceFilter(typeof(LogActionActivity))]
        [HttpPost("Delete")]
        public IActionResult Delete(int Id)
        {
            var selectedRent = _dbContext.Rent.FirstOrDefault(x => x.Id == Id && x.IsDeleted != true);

            if (selectedRent == null)
                return NotFound("The Book not found!");
            else
            {
                //_dbContext.Books.Remove(selectedBook);
                selectedRent.IsDeleted = true;
                _dbContext.SaveChanges();

                return Ok("Successfully Removed!");
            }
        }
    }
}
