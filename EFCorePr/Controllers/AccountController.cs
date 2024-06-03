using EFCorePr.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EFCorePr.Controllers
{

    [Route("MyLibrary/[controller]")]
    public class AccountController : Controller
    {
        private readonly BookStoreEFCoreContext _context;

        public AccountController(BookStoreEFCoreContext context) => _context = context;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] string userName, [FromForm] string password)
        {
            var userToAuthenticate = await _context.Customer
                .FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password && !u.IsDeleted);

            if (userToAuthenticate != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userName)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Get", "Books");
            }

            return Ok("Wrong Username or Password!");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("get", "Books");
        }

    }
}
