using EFCorePr.Controllers.Filter;
using EFCorePr.Models;
using EFCorePr.Tools;
using EFCorePr.ViewModels;
using EFCorePr.ViewModels.Customer.Create;
using EFCorePr.ViewModels.Customer.Update;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCorePr.Controllers
{
    [TypeFilter(typeof(ExceptionHandler))]
    [ServiceFilter(typeof(LogActionActivity))]
    [Route("MyLibrary/[controller]")]
    public class CustomerController : Controller
    {
        private readonly BookStoreEFCoreContext _dbContext;

        public CustomerController(BookStoreEFCoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("get-all")]
        public IActionResult Get()
        {
            var customers = _dbContext.Customer.Where(c => !c.IsDeleted)
                .Select(c => new
                {
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    PhoneNum = c.PhoneNum,
                    NationalCOde = c.NationalCode
                });

            

            return Ok(customers);
        }

        [HttpGet("search-user/{Id}")]
        public IActionResult GetByID(int Id)
        {
            var selectedCustomer = _dbContext.Customer.FirstOrDefault(x => x.Id == Id && !x.IsDeleted);

            if (selectedCustomer == null)
                return NotFound("The User Not Found!");

            return Ok(new
            {
                FirstName = selectedCustomer.FirstName,
                LastName = selectedCustomer.LastName,
                PhoneNum = selectedCustomer.PhoneNum,
                NationalCode = selectedCustomer.NationalCode
            });
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Create([FromForm] CreateCustomerViewModel customerView)
        {

            if (ModelState.IsValid)
            {

                _dbContext.Customer.Add(new Customer()
                {
                    FirstName = customerView.FirstName,
                    LastName = customerView.LastName,
                    PhoneNum = customerView.PhoneNum,
                    NationalCode = customerView.NationalCode,
                    Password = customerView.Password,
                    UserName = customerView.NationalCode
                });

                await _dbContext.SaveChangesAsync();

                return Ok();
            }

            return Ok(customerView);
        }


        [HttpGet("edit/{Id}")]
        public async Task<IActionResult> Update(int Id)
        {
            var selectedCustomer = await _dbContext.Customer
                .FirstOrDefaultAsync(c => c.Id == Id && !c.IsDeleted);

            if (selectedCustomer == null)
                return NotFound("Customer Not Found!");

            return Ok(new UpdateCustomerViewModel
            {
                FirstName = selectedCustomer.FirstName,
                LastName = selectedCustomer.LastName,
                PhoneNum = selectedCustomer.PhoneNum,
                NationalCode = selectedCustomer.NationalCode
            });
        }

        [HttpPost("Edit/{Id}")]
        public async Task<IActionResult> Update([FromForm] UpdateCustomerViewModel customerView)
        {
            var selectedCustomer = await _dbContext.Customer
                .FirstOrDefaultAsync(x => x.Id == customerView.Id && !x.IsDeleted);

            if (selectedCustomer == null)
                return NotFound("The User not found!");

            if (ModelState.IsValid)
            {
                selectedCustomer.FirstName = customerView.FirstName;
                selectedCustomer.LastName = customerView.LastName;
                selectedCustomer.PhoneNum = customerView.PhoneNum;
                selectedCustomer.NationalCode = customerView.NationalCode;
                selectedCustomer.Password = customerView.Password;

                await _dbContext.SaveChangesAsync();

                return Ok("Updated!");
            }
            return Ok(customerView);

        }


        [HttpPost("Delete/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var customer = await _dbContext.Customer.FirstOrDefaultAsync(x => x.Id == Id && !x.IsDeleted);

            if (customer == null)
                return Ok("The User Not Found!");

            customer.IsDeleted = true;

            await _dbContext.SaveChangesAsync();

            return Ok("Deleted!");
        }
    }
}
