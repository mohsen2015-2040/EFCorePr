using EFCorePr.Controllers.Filter;
using EFCorePr.Models;
using EFCorePr.Services;
using EFCorePr.ViewModels;
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

        //Actions
        public ActionResult Index()
        {
            var guideMessage = _generateGuide.GenerateMessage(typeof(Publisher));
            return Ok(guideMessage);
        }


        [ServiceFilter(typeof(LogActionActivity))]
        [HttpGet("Get-All")]
        public IActionResult Get()
        {
            var publishers = _dbContext.Publisher.Where(p => !p.IsDeleted);

            List<PublisherViewData> publisherViews = new List<PublisherViewData>();
            foreach (var publisher in publishers)
            {
                publisherViews.Add(new PublisherViewData { FullName = publisher.FullName});
            }

            return Ok(publisherViews);
        }

        [ServiceFilter(typeof(LogActionActivity))]
        [HttpGet("search-publisher/{Id}")]
        public IActionResult GetByID(int Id)
        {
            var selectedPublisher = _dbContext.Publisher.FirstOrDefault(x => x.Id == Id && !x.IsDeleted);

            if (selectedPublisher == null)
                return NotFound("The Publisher Not Found!");

              
            return Ok(new PublisherViewData { FullName = selectedPublisher.FullName});
        }


        [ServiceFilter(typeof(LogActionActivity))]
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromForm] PublisherViewData publisherView)
        {
            await _dbContext.Publisher.AddAsync(new Publisher() { FullName = publisherView.FullName});
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Get");
        }



        [HttpGet("edit/{Id}")]
        public async Task<IActionResult> Update(int Id)
        {
            var selectedPublisher = await _dbContext.Publisher.FirstOrDefaultAsync(x => x.Id == Id && !x.IsDeleted);

            if (selectedPublisher == null)
                return NotFound("The Publisher Not Found!");

            return Ok(new PublisherViewData { FullName = selectedPublisher.FullName});
        }

        [ServiceFilter(typeof(LogActionActivity))]
        [HttpPost("Edit/{Id}")]
        public async Task<IActionResult> Update(int Id,[FromForm] PublisherViewData publisherView)
        {
            var selectedPublisher = await _dbContext.Publisher.FirstOrDefaultAsync(x => x.Id == Id && !x.IsDeleted);

            if (selectedPublisher == null)
                return NotFound("The Publisher not found!");
            
            if (ModelState.IsValid)
            {
                selectedPublisher.FullName = publisherView.FullName;
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Get");
            }
         
            return Ok(publisherView);
        }



        [ServiceFilter(typeof(LogActionActivity))]
        [HttpPost("Delete/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var selectedPublisher = await _dbContext.Publisher.FirstOrDefaultAsync(x => x.Id == Id && !x.IsDeleted);

            if (selectedPublisher == null)
                return NotFound("The Publisher not found!");

            
            selectedPublisher.IsDeleted = true;
    
            await _dbContext.SaveChangesAsync();
                
            return RedirectToAction("Get");
              
        }
    }
}
