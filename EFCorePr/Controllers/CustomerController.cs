using EFCorePr.Controllers.Filter;
using EFCorePr.Models;
using EFCorePr.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace EFCorePr.Controllers
{
    [TypeFilter(typeof(ExceptionHandler))]
    [Route("MyLibrary/[controller]")]
    public class CustomerController : Controller
    {
        private readonly BookStoreEFCoreContext _dbContext;
        private readonly IGenerateGuideToRoutsService _generateGuide;
       
        public CustomerController(BookStoreEFCoreContext dbContext, IGenerateGuideToRoutsService generateGuide, ILogger<CustomerController> logger)
        {
            _dbContext = dbContext;
            _generateGuide = generateGuide;
        }


        public ActionResult Index()
        {
            var guideMessage = _generateGuide.GenerateMessage(typeof(Customer));
            
            return Ok(guideMessage);
        }

        [ServiceFilter(typeof(LogActionActivity))]
        [HttpGet("get-all")]
        public IActionResult Get()
        {
            var customers = from c in _dbContext.Customer
                             where (c.IsDeleted != true)
                             select c;

            return Ok(customers.ToArray());
        }

        [ServiceFilter(typeof(LogActionActivity))]
        [HttpGet("search-user")]
        public IActionResult GetByID(int Id)
        {
            var selectedCustomer = _dbContext.Customer.FirstOrDefault(x => x.Id == Id && x.IsDeleted != true);

            if(selectedCustomer == null)
                return NotFound("The User Not Found!");
            else
                return Ok(selectedCustomer);
        }

        [ServiceFilter(typeof(LogActionActivity))]
        [HttpPost("Add")]
        public async Task<IActionResult> Create(Customer customer)
        {
            _dbContext.Customer.Add(customer);
            await _dbContext.SaveChangesAsync();

            return Ok("Successfully added.");
        }

        [ServiceFilter(typeof(LogActionActivity))]
        [HttpPost("Edit")]
        public async Task<IActionResult> Update(int Id, [Bind("Id", "FirstName", "LastName", "PhoneNum")] Customer customer)
        {
            var selectedCustomer = _dbContext.Customer.FirstOrDefault(x => x.Id == Id && x.IsDeleted != true);

            if (selectedCustomer == null)
                return NotFound("The User not found!");
            else if (Id != customer.Id)
                return BadRequest("The provided Id doesn't match the Id in the users data!");
            else if (ModelState.IsValid)
            {
                selectedCustomer.FirstName = customer.FirstName;
                selectedCustomer.LastName = customer.LastName;
                selectedCustomer.PhoneNum = customer.PhoneNum;
                await _dbContext.SaveChangesAsync();

                return Ok("Successfully Updated");
            }
            else
                return Ok(customer);
                
        }

        [ServiceFilter(typeof(LogActionActivity))]
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(int Id)
        {
            var customer = _dbContext.Customer.FirstOrDefault(x => x.Id == Id && x.IsDeleted != true);

            if (customer == null)
                return Ok("The User Not Found!");
            else
            {
                // _dbContext.Customer.Remove(customer);
                customer.IsDeleted = true;
                await _dbContext.SaveChangesAsync();

                return Ok("Successfully Removed!");
            }
        }
    }
}
