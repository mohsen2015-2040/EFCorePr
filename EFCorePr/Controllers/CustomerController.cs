using EFCorePr.Controllers.Filter;
using EFCorePr.Models;
using EFCorePr.Services;
using EFCorePr.Tools;
using EFCorePr.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCorePr.Controllers
{
    [TypeFilter(typeof(ExceptionHandler))]
    [Route("MyLibrary/[controller]")]
    public class CustomerController : Controller
    {
        private readonly BookStoreEFCoreContext _dbContext;
        private readonly IGenerateGuideToRoutsService _generateGuide;

        public CustomerController(BookStoreEFCoreContext dbContext,
            IGenerateGuideToRoutsService generateGuide,
            ILogger<CustomerController> logger)
        {
            _dbContext = dbContext;
            _generateGuide = generateGuide;
        }


        //Actions
        public ActionResult Index()
        {
            var guideMessage = _generateGuide.GenerateMessage(typeof(Customer));


            return Ok(guideMessage);
        }

        [ServiceFilter(typeof(LogActionActivity))]
        [HttpGet("get-all")]
        public IActionResult Get()
        {
            var customers = _dbContext.Customer.Where(c => !c.IsDeleted);

            List<CustomerViewData> customerViews = new List<CustomerViewData>();
            foreach (var customer in customers)
            {
                customerViews.Add(new CustomerViewData
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    PhoneNum = customer.PhoneNum,
                    NationalCode = customer.NationalCode
                });
            }

            return Ok(customerViews);
        }

        [ServiceFilter(typeof(LogActionActivity))]
        [HttpGet("search-user/{Id}")]
        public IActionResult GetByID(int Id)
        {
            var selectedCustomer = _dbContext.Customer.FirstOrDefault(x => x.Id == Id && !x.IsDeleted);

            if (selectedCustomer == null)
                return NotFound("The User Not Found!");

            return Ok(new CustomerViewData
            {
                FirstName = selectedCustomer.FirstName,
                LastName = selectedCustomer.LastName,
                PhoneNum = selectedCustomer.PhoneNum,
                NationalCode = selectedCustomer.NationalCode
            });
        }

        [ServiceFilter(typeof(LogActionActivity))]
        [HttpPost("Add")]
        public async Task<IActionResult> Create([FromForm] CustomerViewData customerView)
        {
            //var validateResult = _userValidator.Validate();

            if (ModelState.IsValid)
            {
                await _dbContext.Customer.AddAsync(new Customer()
                {
                    FirstName = customerView.FirstName,
                    LastName = customerView.LastName,
                    PhoneNum = customerView.PhoneNum,
                    NationalCode = customerView.NationalCode
                });

                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Get");
            }

            return Ok(customerView);
        }


        [ServiceFilter(typeof(LogActionActivity))]
        [HttpGet("edit/{Id}")]
        public async Task<IActionResult> Update(int Id)
        {
            var selectedCustomer = await _dbContext.Customer.FirstOrDefaultAsync(c => c.Id == Id && !c.IsDeleted);

            if (selectedCustomer == null)
                return NotFound("The Customer Not Found!");

            return Ok(new CustomerViewData
            {
                FirstName = selectedCustomer.FirstName,
                LastName = selectedCustomer.LastName,
                PhoneNum = selectedCustomer.PhoneNum,
                NationalCode = selectedCustomer.NationalCode
            });
        }

        [ServiceFilter(typeof(LogActionActivity))]
        [HttpPost("Edit/{Id}")]
        public async Task<IActionResult> Update(int Id, [FromForm] CustomerViewData customerView)
        {
            var selectedCustomer = await _dbContext.Customer.FirstOrDefaultAsync(x => x.Id == Id && !x.IsDeleted);

            if (selectedCustomer == null)
                return NotFound("The User not found!");

            if (ModelState.IsValid)
            {
                selectedCustomer.FirstName = customerView.FirstName;
                selectedCustomer.LastName = customerView.LastName;
                selectedCustomer.PhoneNum = customerView.PhoneNum;
                selectedCustomer.NationalCode = customerView.NationalCode;

                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Get");
            }
            return Ok(customerView);

        }



        [ServiceFilter(typeof(LogActionActivity))]
        [HttpPost("Delete/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var customer = await _dbContext.Customer.FirstOrDefaultAsync(x => x.Id == Id && !x.IsDeleted);

            if (customer == null)
                return Ok("The User Not Found!");

            customer.IsDeleted = true;

            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Get");
        }
    }
}
