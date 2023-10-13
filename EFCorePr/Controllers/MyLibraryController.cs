using EFCorePr.DatabaseContext;
using EFCorePr.Model;
using Microsoft.AspNetCore.Mvc;

namespace EFCorePr.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MyLibraryController : ControllerBase
    {

        private readonly ILogger<MyLibraryController> _logger;
        private readonly BookContext _bookContext;

        public MyLibraryController(ILogger<MyLibraryController> logger, BookContext bookContext)
        {
            _logger = logger;
            _bookContext = bookContext;
        }

        //Create:
        [HttpGet]
        [Route("AddBook")]
        public IActionResult AddBook()
        {
            _bookContext.Add(new Model.Book("Learn English", "Toturial", "Mohsen Bazazan"));
            _bookContext.SaveChanges();
           
            
            return Ok("Successfully added.");
        }

        //Update:
        [HttpGet]
        [Route("EditBook")]
        public IActionResult UpdateBook()
        {
            var selectedBook = _bookContext.Books.FirstOrDefault(b => b.Id == 5);

            if(selectedBook != null)
            {
                selectedBook.Author = "JJ M (edited)";
                selectedBook.Title = "Historical (edited)";
                
                _bookContext.SaveChanges();
                return Ok("Successfully updated.");
            }else
                return Ok("Book not found!");
        }

        //Delete:
        [HttpGet]
        [Route("DeleteBook")]
        public IActionResult DeleteBook()
        {
            var selectedBook = _bookContext.Books.FirstOrDefault(b => b.Id == 2);

            if(selectedBook != null && !selectedBook.IsDeleted)
            {
                selectedBook.IsDeleted = true;
                _bookContext.SaveChanges();

                return Ok("Successfully deleted.");
            }else
                return Ok("Book not found!");

        }

        //Read:
        [HttpGet]
        [Route("GetBooks")]
        public IActionResult GetBook() 
        {
            var books = _bookContext.Books.Where(x => !x.IsDeleted);

            return Ok(books);
        }
    }
}