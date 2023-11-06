//using EFCorePr.DatabaseContext;
using EFCorePr.Controllers.Filter;
using EFCorePr.Models;
using EFCorePr.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace EFCorePr.Controllers
{

    [Route("MyLibrary/[controller]")]
    public class CustomerController : Controller
    {
        private readonly BookStoreEFCoreContext _dbContext;
        private readonly IGenerateGuideToRoutsService _generateGuide;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(BookStoreEFCoreContext dbContext, IGenerateGuideToRoutsService generateGuide, ILogger<CustomerController> logger)
        {
            _dbContext = dbContext;
            _generateGuide = generateGuide;
            _logger = logger;
        }


        public ActionResult Index()
        {
            var guideMessage = _generateGuide.GenerateMessage(typeof(Customer));
            
            return Ok(guideMessage);
        }

        [ServiceFilter(typeof(ExceptionHandler))]
        [HttpGet]
        [Route("GetCustomers")]
        public IActionResult Get()
        {
            return Ok(_dbContext.Customer);
        }

        [ServiceFilter(typeof(ExceptionHandler))]
        [Route("Add")]
        public IActionResult Create()
        {
            _dbContext.Customer.Add(new Models.Customer { FirstName = "Alireza", LastName = "Mohammadi", PhoneNum = "09121445758" });
            _dbContext.SaveChanges();

            return Ok("Successfully added.");
        }

        [ServiceFilter(typeof(ExceptionHandler))]
        [Route("Edit")]
        public IActionResult Update()
        {
            var customer = _dbContext.Customer.FirstOrDefault(x => x.Id == 4);
            if (customer != null)
            {
                customer.PhoneNum = "09912307067";
                _dbContext.SaveChanges();

                return Ok("Successfully Updated");
            }
            else
                return Ok("User Not Found!");
        }

        [ServiceFilter(typeof(ExceptionHandler))]
        [Route("Delete")]
        public IActionResult Delete()
        {
            var customer = _dbContext.Customer.FirstOrDefault(x => x.Id == 3);

            if (customer != null)
            {
                _dbContext.Customer.Remove(customer);

                _dbContext.SaveChanges();

                return Ok("Successfully Removed!");
            }
            else
                return Ok("User Not Found!");
        }
    }
}
