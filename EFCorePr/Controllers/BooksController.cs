//using EFCorePr.DatabaseContext;
using EFCorePr.Controllers.Filter;
using EFCorePr.Models;
using EFCorePr.Services;
using Microsoft.AspNetCore.Mvc;

namespace EFCorePr.Controllers
{
    [Route("MyLibrary/[controller]")]
    public class BooksController : Controller
    {
        private readonly BookStoreEFCoreContext _dbContext;
        private readonly IGenerateGuideToRoutsService _generateGuide;
        private readonly ILogger<BooksController> _logger;

        public BooksController(BookStoreEFCoreContext bookStoreContext, IGenerateGuideToRoutsService generateGuide, ILogger<BooksController> logger)
        {
            _dbContext = bookStoreContext;
            _generateGuide = generateGuide;
            _logger = logger;
        }

        public ActionResult Index()
        {
            var guideMessage = _generateGuide.GenerateMessage(typeof(Books));
            return Ok(guideMessage);
        }

        [ServiceFilter(typeof(ExceptionHandler))]
        [HttpGet]
        [Route("GetBooks")]
        public IActionResult Get()
        {
            var books = from b in _dbContext.Books where (!b.IsDeleted)
                    join p in _dbContext.Publisher
                    on b.PublisherId equals p.Id
                    select new { b, p };

            string response = "";

            foreach(var item in books)
            {
                response += $"[Name: {item.b.Title} * Description: {item.b.Description} * ISBN: {item.b.Isbn} * Publisher: {item.b.Publisher.FullName} ] - ";   
            }
            return Ok(response);
        }


        [ServiceFilter(typeof(ExceptionHandler))]
        [Route("add")]
        public IActionResult Add()
        {

            _dbContext.Books.Add(new Models.Books { Title = "Shahname", Description = "Tutorial", Isbn = "1234567999", PublisherId = 6 });
            _dbContext.SaveChanges();

            return Ok("Successfully added.");

        }

        [ServiceFilter(typeof(ExceptionHandler))]
        [Route("Edit")]
        public IActionResult Update()
        {
            var selecedBook = _dbContext.Books.FirstOrDefault(x => x.Id == 14);
            if (selecedBook != null)
            {
                selecedBook.Description = "Tutorial";
                _dbContext.SaveChanges();

                return Ok("Successfully Updated");
            }
            else
                return Ok("Book Not Found!");
        }

        [Route("Delete")]
        public IActionResult Delete()
        {
            var selectedBook = _dbContext.Books.FirstOrDefault(x => x.Id == 13);

            if (selectedBook != null)
            {
                _dbContext.Books.Remove(selectedBook);
                _dbContext.SaveChanges();

                return Ok("Successfully Removed!");
            }
            else
                return Ok("Book Not Found!");
        }
    }
}
