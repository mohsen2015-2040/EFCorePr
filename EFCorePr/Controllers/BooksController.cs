//using EFCorePr.DatabaseContext;
using EFCorePr.Controllers.Filter;
using EFCorePr.Models;
using EFCorePr.Services;
using Microsoft.AspNetCore.Mvc;

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

        public ActionResult Index()
        {
            var guideMessage = _generateGuide.GenerateMessage(typeof(Books));
            return Ok(guideMessage);
        }

        [ServiceFilter(typeof(LogActionActivity))]
        [HttpGet("get-all")]
        public IActionResult Get()
        {
            var books = from b in _dbContext.Books
                             where (b.IsDeleted != true)
                             select b;

            return Ok(books.ToArray());
        }

        [ServiceFilter(typeof(LogActionActivity))]
        [HttpGet("search-book")]
        public IActionResult GetByID(int Id)
        {
            var selectedBook = _dbContext.Books.FirstOrDefault(x => x.Id == Id && x.IsDeleted != true);

            if (selectedBook == null)
                return NotFound("The Book not found!");
            else
                return Ok(selectedBook);
        }

        [ServiceFilter(typeof(LogActionActivity))]
        [HttpPost("add")]
        public async Task<IActionResult> Add(Books book)
        {
            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync();

            return Ok("Successfully added.");
        }

        [ServiceFilter(typeof(LogActionActivity))]
        [HttpPost("edit")]
        public async Task<IActionResult> Update(int Id, [Bind("Id", "Title", "Description", "Isbn")] Books book)
        {
            var selecedBook = _dbContext.Books.FirstOrDefault(x => x.Id == Id && x.IsDeleted != true);

            if (selecedBook == null)
                return NotFound("The Book not found!");
            else if (Id != book.Id)
                return BadRequest("The provided Id does not match the Id in the book data.");

            else if (ModelState.IsValid)
            {
                selecedBook.Title = book.Title;
                selecedBook.Description = book.Description;
                selecedBook.Isbn = book.Isbn;
                //_dbContext.Update(book);
                await _dbContext.SaveChangesAsync();

                return Ok("Successfully Updated");
            }
            else
                return Ok(book);
        }

        [ServiceFilter(typeof(LogActionActivity))]
        [HttpPost("Delete")]
        public IActionResult Delete(int Id)
        {
            var selectedBook = _dbContext.Books.FirstOrDefault(x => x.Id == Id && x.IsDeleted != true);

            if (selectedBook == null)
                return NotFound("The Book not found!");
            else
            {
                //_dbContext.Books.Remove(selectedBook);
                selectedBook.IsDeleted = true;
                _dbContext.SaveChanges();

                return Ok("Successfully Removed!");
            }      
        }
    }
}
