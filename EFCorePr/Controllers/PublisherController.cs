using EFCorePr.Controllers.Filter;
using EFCorePr.Models;
using EFCorePr.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCorePr.Controllers
{

    [Route("MyLibrary/[controller]")]
    public class PublisherController : Controller
    {
        private readonly BookStoreEFCoreContext _dbContext;
        private readonly IGenerateGuideToRoutsService _generateGuide;
        private readonly ILogger<PublisherController> _logger;

        public PublisherController(BookStoreEFCoreContext bookStoreEFCoreContext, IGenerateGuideToRoutsService generateGuide, ILogger<PublisherController> logger)
        {
            _dbContext = bookStoreEFCoreContext;
            _generateGuide = generateGuide;
            _logger = logger;
        }

        public ActionResult Index()
        {
            var guideMessage = _generateGuide.GenerateMessage(typeof(Publisher));
            return Ok(guideMessage);
        }


        [ServiceFilter(typeof(ExceptionHandler))]
        [Route("GetPublishers")]
        public ActionResult Get()
        {
            return Ok(_dbContext.Publisher);
        }


        [ServiceFilter(typeof(ExceptionHandler))]
        [Route("Add")]
        public IActionResult Add()
        {
            _dbContext.Publisher.Add(new Models.Publisher { FullName = "Iran" });
            _dbContext.SaveChanges();

            return Ok("successfully added");
        }

        [ServiceFilter(typeof(ExceptionHandler))]
        [Route("Edit")]
        public IActionResult Update()
        {
            var selectedPublisher = _dbContext.Publisher.FirstOrDefault(x => x.Id == 4);

            if (selectedPublisher != null)
            {
                selectedPublisher.FullName = "Asman";
                _dbContext.SaveChanges();

                return Ok("Successfully Updated");
            }
            else
                return Ok("Publisher Not Found!");
        }


        [ServiceFilter(typeof(ExceptionHandler))]
        [Route("Delete")]
        public IActionResult Delete()
        {
            var selectedPublisher = _dbContext.Publisher.FirstOrDefault(x => x.Id == 2);

            if (selectedPublisher != null)
            {
                _dbContext.Publisher.Remove(selectedPublisher);

                _dbContext.SaveChanges();

                return Ok("Successfully Removed!");
            }
            else
                return Ok("Publisher Not Found!");
        }
    }
}
