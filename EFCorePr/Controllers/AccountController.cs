using EFCorePr.Models;
using EFCorePr.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EFCorePr.Controllers
{

    [Route("MyLibrary/[controller]")]
    public class AccountController : Controller
    {
        private readonly BookStoreEFCoreContext _context;
        private readonly JWTTokenGenerator _jwtTokenGenerator;

        public AccountController(BookStoreEFCoreContext context, JWTTokenGenerator tokenGenerator) 
        {
            _context = context;
            _jwtTokenGenerator = tokenGenerator; 
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] string userName, [FromForm] string password)
        {
            var userToAuthenticate = await _context.Customer
                .FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password && !u.IsDeleted);

            if (userToAuthenticate != null)
            {
                //var claims = new List<Claim>
                //{
                //    new Claim(ClaimTypes.Name, userName)
                //};

                //var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                //var principal = new ClaimsPrincipal(identity);

                //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                var token = _jwtTokenGenerator.GenerateToken(userName, DateTime.Now +  TimeSpan.FromDays(1));

                Response.Cookies.Append("jwt", token);

                return Ok(new { Token = token });
            }

            return Ok("Wrong Username or Password!");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete("jwt");

            return RedirectToAction("get", "Books");
        }

    }
}
