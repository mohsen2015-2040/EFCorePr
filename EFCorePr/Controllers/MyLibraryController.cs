
using EFCorePr.Controllers.Filter;
using EFCorePr.Models;
using EFCorePr.Services;
using Microsoft.AspNetCore.Mvc;

namespace EFCorePr.Controllers
{
    [TypeFilter(typeof(ExceptionHandler))]
    [ApiController]
    [Route("[controller]")]
    public class MyLibraryController : ControllerBase
    {
        private readonly IGenerateGuideToRoutsService _generateGuide;

        public MyLibraryController(IGenerateGuideToRoutsService generateGuide)
        {
            _generateGuide = generateGuide;
        }


        public IActionResult Index()
        {
            var guideMessage = _generateGuide.GenerateMessage(this.GetType());
            
            return Ok(guideMessage);
        }

    }
}