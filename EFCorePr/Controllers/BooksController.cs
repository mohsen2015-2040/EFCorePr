//using EFCorePr.DatabaseContext;
using EFCorePr.Controllers.Filter;
using EFCorePr.Models;
using EFCorePr.Services;
using EFCorePr.Tools;
using EFCorePr.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCorePr.Controllers
{
    [TypeFilter(typeof(ExceptionHandler))]
    [Route("MyLibrary/[controller]")]
    public class BooksController : Controller
    {
        private readonly BookStoreEFCoreContext _dbContext;
        private readonly IGenerateGuideToRoutsService _generateGuide;

        public BooksController(BookStoreEFCoreContext bookStoreContext, IGenerateGuideToRoutsService generateGuide, ILogger<BooksController> logger)
        {
            _dbContext = bookStoreContext;
            _generateGuide = generateGuide;
        }

        //Actions
        public ActionResult Index()
        {
            var guideMessage = _generateGuide.GenerateMessage(typeof(Book));
            return Ok(guideMessage);
        }

        [ServiceFilter(typeof(LogActionActivity))]
        [HttpGet("get-all")]
        public IActionResult Get()
        {
            var books = _dbContext.Book.Where(b => !b.IsDeleted).ToList();

            List<BookViewData> bookViews = new List<BookViewData>();
            foreach (var b in books)
            {
                bookViews.Add(new BookViewData
                {
                    Title = b.Title,
                    Description = b.Description,
                    PublisherName = b.Publisher.FullName,
                    Isbn = b.Isbn
                });
            }

            return Ok(bookViews);
        }

        [ServiceFilter(typeof(LogActionActivity))]
        [HttpGet("search-book/{Id}")]
        public async Task<IActionResult> GetByID(int Id)
        {
            var selectedBook = await _dbContext.Book.FirstOrDefaultAsync(x => x.Id == Id && !x.IsDeleted);

            if (selectedBook == null)
                return NotFound("The Book not found!");

            return Ok(new BookViewData
            {
                Title = selectedBook.Title,
                Description = selectedBook.Description,
                PublisherName = selectedBook.Publisher.FullName,
                Isbn = selectedBook.Isbn
            });
        }

        [ServiceFilter(typeof(LogActionActivity))]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] BookViewData bookView)
        {
            var bookViewPublisher = _dbContext.Publisher.FirstOrDefault(p => p.FullName == bookView.PublisherName && !p.IsDeleted);

            if (bookViewPublisher == null)
                return NotFound("Publisher Is Not Valid!");

            if (ModelState.IsValid)
            {
                await _dbContext.Book.AddAsync(new Book
                {
                    Title = bookView.Title,
                    Isbn = bookView.Isbn,
                    Description = bookView.Description,
                    PublisherId = bookViewPublisher.Id
                });

                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Get");
            }
            return Ok(bookView);
        }


        [ServiceFilter(typeof(LogActionActivity))]
        [HttpGet("edit/{Id}")]
        public async Task<IActionResult> Update(int Id)
        {
            var selectedBook = await _dbContext.Book.FirstOrDefaultAsync(b => b.Id == Id && !b.IsDeleted);

            if (selectedBook == null)
                return NotFound("The Book Not Found!");

            return Ok(new BookViewData
            {
                Title = selectedBook.Title,
                Description = selectedBook.Description,
                Isbn = selectedBook.Isbn,
                PublisherName = selectedBook.Publisher.FullName
            });
        }

        [ServiceFilter(typeof(LogActionActivity))]
        [HttpPost("edit/{Id}")]
        public async Task<IActionResult> Update(int Id, [FromForm] BookViewData bookView)
        {
            var bookViewPublisher = await _dbContext.Publisher.FirstOrDefaultAsync(p => p.FullName == bookView.PublisherName && !p.IsDeleted);
            var selecedBook = await _dbContext.Book.FirstOrDefaultAsync(x => x.Id == Id && !x.IsDeleted);

            if (bookViewPublisher == null || selecedBook == null)
                return NotFound("Not Found!");

            if (ModelState.IsValid)
            {
                selecedBook.Title = bookView.Title;
                selecedBook.Description = bookView.Description;
                selecedBook.Isbn = bookView.Isbn;
                selecedBook.PublisherId = bookViewPublisher.Id;

                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Get");
            }
            return Ok(bookView);
        }



        [ServiceFilter(typeof(LogActionActivity))]
        [HttpPost("Delete/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var selectedBook = await _dbContext.Book.FirstOrDefaultAsync(x => x.Id == Id && !x.IsDeleted);

            if (selectedBook == null)
                return NotFound("The Book not found!");


            selectedBook.IsDeleted = true;
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Get");
        }
    }
}
