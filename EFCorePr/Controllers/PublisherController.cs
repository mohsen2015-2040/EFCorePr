using EFCorePr.Controllers.Filter;
using EFCorePr.Models;
using EFCorePr.ViewModels;
using EFCorePr.ViewModels.Publisher.Create;
using EFCorePr.ViewModels.Publisher.Update;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCorePr.Controllers
{
    [TypeFilter(typeof(ExceptionHandler))]
    [ServiceFilter(typeof(LogActionActivity))]
    [Route("MyLibrary/[controller]")]
    public class PublisherController : Controller
    {
        private readonly BookStoreContext _dbContext;

        public PublisherController(BookStoreContext bookStoreEFCoreContext)
        {
            _dbContext = bookStoreEFCoreContext;
        }

        
        [HttpGet("Get-All")]
        public IActionResult Get()
        {
            var publishers = _dbContext.Publisher.Where(p => !p.IsDeleted)
                .Select(p => new
                {
                    Fullname = p.FullName
                }).ToList();

            return Ok(publishers);
        }

        [HttpGet("search-publisher/{Id}")]
        public IActionResult GetByID(int Id)
        {
            var selectedPublisher = _dbContext.Publisher
                .FirstOrDefault(x => x.Id == Id && !x.IsDeleted);

            if (selectedPublisher == null)
                return NotFound("Publisher Not Found!");

              
            return Ok(new { FullName = selectedPublisher.FullName});
        }


        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromForm] CreatePublisherViewModel publisherView)
        {
            _dbContext.Publisher.Add(new Publisher() { FullName = publisherView.FullName});
            await _dbContext.SaveChangesAsync();

            return Ok();
        }



        [HttpGet("edit/{Id}")]
        public async Task<IActionResult> Update(int Id)
        {
            var selectedPublisher = await _dbContext.Publisher
                .FirstOrDefaultAsync(x => x.Id == Id && !x.IsDeleted);

            if (selectedPublisher == null)
                return NotFound("Publisher Not Found!");

            return Ok(new UpdatePublisherViewmodel { FullName = selectedPublisher.FullName});
        }

        [HttpPost("Edit/{Id}")]
        public async Task<IActionResult> Update([FromForm] UpdatePublisherViewmodel publisherView)
        {
            var selectedPublisher = await _dbContext.Publisher
                .FirstOrDefaultAsync(x => x.Id == publisherView.Id && !x.IsDeleted);

            if (selectedPublisher == null)
                return NotFound("Publisher not found!");
            
            if (ModelState.IsValid)
            {
                selectedPublisher.FullName = publisherView.FullName;
                await _dbContext.SaveChangesAsync();

                return Ok();
            }
         
            return Ok(publisherView);
        }


        [HttpPost("Delete/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var selectedPublisher = await _dbContext.Publisher
                .FirstOrDefaultAsync(x => x.Id == Id && !x.IsDeleted);

            if (selectedPublisher == null)
                return NotFound("Publisher not found!");

            
            selectedPublisher.IsDeleted = true;
    
            await _dbContext.SaveChangesAsync();
                
            return Ok();
              
        }
    }
}
