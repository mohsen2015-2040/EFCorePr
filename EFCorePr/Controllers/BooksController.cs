//using EFCorePr.DatabaseContext;
using EFCorePr.Controllers.Filter;
using EFCorePr.Models;
using EFCorePr.Tools;
using EFCorePr.ViewModels;
using EFCorePr.ViewModels.Book.Create;
using EFCorePr.ViewModels.Book.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCorePr.Controllers
{

    //[Authorize]
    [TypeFilter(typeof(ExceptionHandler))]
    [Route("MyLibrary/[controller]")]
    public class BooksController : Controller
    {
        private readonly BookStoreEFCoreContext _dbContext;

        public BooksController(BookStoreEFCoreContext bookStoreContext)
        {
            _dbContext = bookStoreContext;
        }

        [Authorize]
        [ServiceFilter(typeof(LogActionActivity))]
        [HttpGet("get-all")]
        public IActionResult Get()
        {
            var books = _dbContext.Book.Where(b => !b.IsDeleted).Select(b => new
            {
                Title = b.Title,
                Isbn = b.Isbn,
                Description = b.Description
            }).ToList();

            return Ok(books);
        }

        [ServiceFilter(typeof(LogActionActivity))]
        [HttpGet("search-book/{Id}")]
        public async Task<IActionResult> GetByID(int Id)
        {
            var selectedBook = await _dbContext.Book.FirstOrDefaultAsync(x => x.Id == Id && !x.IsDeleted);

            if (selectedBook == null)
                return NotFound("The Book not found!");

            return Ok(new
            {
                Title = selectedBook.Title,
                Description = selectedBook.Description,
                PublisherName = selectedBook.Publisher.FullName,
                Isbn = selectedBook.Isbn
            });
        }

        [ServiceFilter(typeof(LogActionActivity))]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] CreateBookViewModel bookView)
        {
            var bookViewPublisher = await _dbContext.Publisher
                .FirstOrDefaultAsync(p => p.FullName == bookView.PublisherName && !p.IsDeleted);

            if (bookViewPublisher == null)
            {
                bookViewPublisher = new Publisher { FullName = bookView.PublisherName };

                _dbContext.Publisher.Add(bookViewPublisher);

                await _dbContext.SaveChangesAsync();
            }

            if (ModelState.IsValid)
            {
                _dbContext.Book.Add(new Book
                {
                    Title = bookView.Title,
                    Isbn = bookView.Isbn,
                    Description = bookView.Description,
                    PublisherId = bookViewPublisher.Id
                });

                await _dbContext.SaveChangesAsync();

                return Ok();
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

            return Ok(new UpdateBookViewModel
            {
                Title = selectedBook.Title,
                Description = selectedBook.Description,
                Isbn = selectedBook.Isbn,
                PublisherName = selectedBook.Publisher.FullName
            });
        }

        [ServiceFilter(typeof(LogActionActivity))]
        [HttpPost("edit/{Id}")]
        public async Task<IActionResult> Update([FromForm] UpdateBookViewModel bookView)
        {
            var bookViewPublisher = await _dbContext.Publisher
                .FirstOrDefaultAsync(p => p.FullName == bookView.PublisherName && !p.IsDeleted);
            
            var selecedBook = await _dbContext.Book
                .FirstOrDefaultAsync(x => x.Id == bookView.Id && !x.IsDeleted);


            if (selecedBook == null)
                return NotFound("Not Found!");

            if (bookViewPublisher == null)
            {
                bookViewPublisher = new Publisher { FullName = bookView.PublisherName };

                _dbContext.Add(bookViewPublisher);
                await _dbContext.SaveChangesAsync();
            }

            if (ModelState.IsValid)
            {
                selecedBook.Title = bookView.Title;
                selecedBook.Description = bookView.Description;
                selecedBook.Isbn = bookView.Isbn;
                selecedBook.PublisherId = bookViewPublisher.Id;

                await _dbContext.SaveChangesAsync();

                return Ok();
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

            return Ok("Deleted!");
        }
    }
}
