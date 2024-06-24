using EFCorePr.Controllers.Filter;
using EFCorePr.Models;
using EFCorePr.ViewModels.Customer.Create;
using EFCorePr.ViewModels.Customer.Update;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCorePr.Controllers
{
    [TypeFilter(typeof(ExceptionHandler))]
    [ServiceFilter(typeof(LogActionActivity))]
    [Route("MyLibrary/[controller]")]
    public class CustomerController : Controller
    {
        private readonly BookStoreContext _dbContext;

        public CustomerController(BookStoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Authorize]
        [HttpGet("get-all")]
        public IActionResult Get()
        {
            var customers = _dbContext.Customer.Where(c => !c.IsDeleted)
                .Select(c => new
                {
                    FirstName = c.Fname,
                    LastName = c.Lname,
                    PhoneNum = c.PhoneNum,
                    Address = c.Address
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
                FirstName = selectedCustomer.Fname,
                LastName = selectedCustomer.Lname,
                PhoneNum = selectedCustomer.PhoneNum,
                Address = selectedCustomer.Address
            });
        }

        [AllowAnonymous]
        [HttpPost("Add")]
        public async Task<IActionResult> Create([FromForm] CreateCustomerViewModel customerView)
        {

            if (ModelState.IsValid)
            {

                var customerToAdd = _dbContext.Customer.Add(new Customer()
                {
                    Fname = customerView.FirstName,
                    Lname = customerView.LastName,
                    PhoneNum = customerView.PhoneNum,
                    Password = customerView.Password,
                    UserName = customerView.PhoneNum,
                    Address = customerView.Address
                });
                await _dbContext.SaveChangesAsync();

                var cartToAdd = _dbContext.Cart.Add(new Cart()
                {
                    UserId = customerToAdd.Entity.Id
                });
                await _dbContext.SaveChangesAsync();

                customerToAdd.Entity.CartId = cartToAdd.Entity.Id;

                await _dbContext.SaveChangesAsync();

                return Ok();
            }

            return Ok(customerView);
        }

        [Authorize]
        [HttpGet("edit/{Id}")]
        public async Task<IActionResult> Update(int Id)
        {
            var selectedCustomer = await _dbContext.Customer
                .FirstOrDefaultAsync(c => c.Id == Id && !c.IsDeleted);

            if (selectedCustomer == null)
                return NotFound("Customer Not Found!");

            return Ok(new UpdateCustomerViewModel
            {
                FirstName = selectedCustomer.Fname,
                LastName = selectedCustomer.Lname,
                PhoneNum = selectedCustomer.PhoneNum,
                Address = selectedCustomer.Address
            });
        }

        [Authorize]
        [HttpPost("Edit/{Id}")]
        public async Task<IActionResult> Update([FromForm] UpdateCustomerViewModel customerView)
        {
            var selectedCustomer = await _dbContext.Customer
                .FirstOrDefaultAsync(x => x.Id == customerView.Id && !x.IsDeleted);

            if (selectedCustomer == null)
                return NotFound("The User not found!");

            if (ModelState.IsValid)
            {
                selectedCustomer.Fname = customerView.FirstName;
                selectedCustomer.Lname = customerView.LastName;
                selectedCustomer.PhoneNum = customerView.PhoneNum;
                //selectedCustomer.Password = customerView.Password;
                selectedCustomer.Address = customerView.Address;

                await _dbContext.SaveChangesAsync();

                return Ok("Updated!");
            }
            return Ok(customerView);

        }

        [Authorize]
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
