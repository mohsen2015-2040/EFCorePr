
using EFCorePr.Controllers.Filter;
using EFCorePr.Models;
using Microsoft.AspNetCore.Mvc;

namespace EFCorePr.Controllers
{
    [TypeFilter(typeof(ExceptionHandler))]
    [ApiController]
    [Route("[controller]")]
    public class MyLibraryController : ControllerBase
    {

        //public MyLibraryController(IGenerateGuideToRoutsService generateGuide)
        //{
        //}


        public IActionResult Index()
        {
            return Ok("Welcome!");
        }

    }
}