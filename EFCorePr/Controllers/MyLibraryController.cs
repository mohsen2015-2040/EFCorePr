
using EFCorePr.Models;
using EFCorePr.Services;
using Microsoft.AspNetCore.Mvc;

namespace EFCorePr.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MyLibraryController : ControllerBase
    {

        private readonly ILogger<MyLibraryController> _logger;
        private readonly BookStoreEFCoreContext _bookContext;
        private readonly IGenerateGuideToRoutsService _generateGuide;

        public MyLibraryController(ILogger<MyLibraryController> logger, BookStoreEFCoreContext bookContext, 
            IGenerateGuideToRoutsService generateGuide)
        {
            _logger = logger;
            _bookContext = bookContext;
            _generateGuide = generateGuide;
        }


        public IActionResult Index()
        {
            var guideMessage = _generateGuide.GenerateMessage(this.GetType());
            
            return Ok(guideMessage);
        }

    }
}