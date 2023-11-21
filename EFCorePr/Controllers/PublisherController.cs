using EFCorePr.Controllers.Filter;
using EFCorePr.Models;
using EFCorePr.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCorePr.Controllers
{
    [TypeFilter(typeof(ExceptionHandler))]
    [Route("MyLibrary/[controller]")]
    public class PublisherController : Controller
    {
        private readonly BookStoreEFCoreContext _dbContext;
        private readonly IGenerateGuideToRoutsService _generateGuide;
        
        public PublisherController(BookStoreEFCoreContext bookStoreEFCoreContext, IGenerateGuideToRoutsService generateGuide, ILogger<PublisherController> logger)
        {
            _dbContext = bookStoreEFCoreContext;
            _generateGuide = generateGuide;
        }

        public ActionResult Index()
        {
            var guideMessage = _generateGuide.GenerateMessage(typeof(Publisher));
            return Ok(guideMessage);
        }


        [ServiceFilter(typeof(LogActionActivity))]
        [HttpGet("Get-All")]
        public IActionResult Get()
        {
            var publishers = from p in _dbContext.Publisher
                             where (p.IsDeleted != true)
                             select p;

            return Ok(publishers.ToArray());
        }

        [ServiceFilter(typeof(LogActionActivity))]
        [HttpGet("search-publisher")]
        public IActionResult GetByID(int Id)
        {
            var selectedPublisher = _dbContext.Publisher.FirstOrDefault(x => x.Id == Id && x.IsDeleted != true);

            if (selectedPublisher == null)
                return NotFound("The Publisher Not Found!");
            else
                return Ok(selectedPublisher);
        }


        [ServiceFilter(typeof(LogActionActivity))]
        [HttpPost("Add")]
        public async Task<IActionResult> Add(Publisher publisher)
        {
            _dbContext.Publisher.Add(publisher);
            await _dbContext.SaveChangesAsync();

            return Ok("successfully added");
        }


        [ServiceFilter(typeof(LogActionActivity))]
        [HttpPost("Edit")]
        public async Task<IActionResult> Update(int Id, [Bind("Id", "FullName")] Publisher publisher)
        {
            var selectedPublisher = _dbContext.Publisher.FirstOrDefault(x => x.Id == Id && x.IsDeleted != true);

            if (selectedPublisher == null)
                return NotFound("The Publisher not found!");
            else if (Id != publisher.Id)
                return BadRequest("The providede Id doesn't match the Id in publishers data!");
            else if (ModelState.IsValid)
            {
                selectedPublisher.FullName = publisher.FullName;
                await _dbContext.SaveChangesAsync();

                return Ok("Successfully Updated");
            }
            else
                return Ok(publisher);
        }


        [ServiceFilter(typeof(LogActionActivity))]
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(int Id)
        {
            var selectedPublisher = _dbContext.Publisher.FirstOrDefault(x => x.Id == Id && x.IsDeleted != true);

            if (selectedPublisher == null)
                return NotFound("The Publisher not found!");
            else
            {
                //_dbContext.Publisher.Remove(selectedPublisher);
                selectedPublisher.IsDeleted = true;

                await _dbContext.SaveChangesAsync();

                return Ok("Successfully Removed!");
            }  
        }
    }
}
