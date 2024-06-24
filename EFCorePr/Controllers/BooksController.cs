//using EFCorePr.DatabaseContext;
using EFCorePr.Controllers.Filter;
using EFCorePr.Models;
using EFCorePr.ViewModels.Book.Create;
using EFCorePr.ViewModels.Book.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCorePr.Controllers
{
    [Authorize]
    [TypeFilter(typeof(ExceptionHandler))]
    [TypeFilter(typeof(LogActionActivity))]
    [Route("MyLibrary/[controller]")]
    public class BooksController : Controller
    {
        private readonly BookStoreContext _dbContext;

        public BooksController(BookStoreContext bookStoreContext)
        {
            _dbContext = bookStoreContext;
        }

        [HttpGet("get-all")]
        public IActionResult Get()
        {
            var books = _dbContext.Book.Where(b => !b.IsDeleted).Select(b => new
            {
                Title = b.Title,
                Isbn = b.Isbn,
                Description = b.Description,
                Price = b.Price,
                PublisherName = b.Publisher.FullName,
                AuthorName = b.AuthorBook.Where(x => x.BookId == b.Id)
                .Select(z => new { name = z.Author.Fname + z.Author.Lname })
            }).ToList();

            return Ok(books);
        }

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
                Isbn = selectedBook.Isbn,
                price = selectedBook.Price,
                AuthorName = selectedBook.AuthorBook.Where(x => x.BookId == selectedBook.Id)
                .Select(z => new {name = z.Author.Fname + z.Author.Lname})
            });
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] CreateBookViewModel bookView)
        {
            var bookViewPublisher = await _dbContext.Publisher
                .FirstOrDefaultAsync(p => p.FullName == bookView.PublisherName && !p.IsDeleted);

            var bookViewAuthor = await _dbContext.Author
                .FirstOrDefaultAsync(a => (a.Fname + a.Lname) == (bookView.AuthorFName + bookView.AuthorLName) 
                && !a.IsDeleted);


            if (bookViewPublisher == null)
            {
                bookViewPublisher = new Publisher { FullName = bookView.PublisherName };

                _dbContext.Publisher.Add(bookViewPublisher);

                await _dbContext.SaveChangesAsync();
            }

            if (bookViewAuthor == null)
            {
                bookViewAuthor = new Author { Fname = bookView.AuthorFName, Lname = bookView.AuthorLName };

                _dbContext.Author.Add(bookViewAuthor);

                await _dbContext.SaveChangesAsync();
            }


            if (ModelState.IsValid)
            {
                var bookToCreate =  _dbContext.Book.Add(new Book
                {
                    Title = bookView.Title,
                    Isbn = bookView.Isbn,
                    Description = bookView.Description,
                    PublisherId = bookViewPublisher.Id,
                    Price = bookView.Price
                });
                await _dbContext.SaveChangesAsync();

                _dbContext.AuthorBook.Add(new AuthorBook
                {
                    AuthorId = bookViewAuthor.Id,
                    BookId = bookToCreate.Entity.Id
                });

                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            return Ok(bookView);
        }


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


        [HttpPost("edit/{Id}")]
        public async Task<IActionResult> Update([FromForm] UpdateBookViewModel bookView)
        {
            var bookViewPublisher = await _dbContext.Publisher
                .FirstOrDefaultAsync(p => p.FullName == bookView.PublisherName && !p.IsDeleted);
            
            var selectedBook = await _dbContext.Book
                .FirstOrDefaultAsync(x => x.Id == bookView.Id && !x.IsDeleted);


            if (selectedBook == null)
                return NotFound("Not Found!");

            if (bookViewPublisher == null)
            {
                bookViewPublisher = new Publisher { FullName = bookView.PublisherName };

                _dbContext.Add(bookViewPublisher);
                await _dbContext.SaveChangesAsync();
            }

            if (ModelState.IsValid)
            {
                selectedBook.Title = bookView.Title;
                selectedBook.Description = bookView.Description;
                selectedBook.Isbn = bookView.Isbn;
                selectedBook.Price = bookView.Price;
                selectedBook.PublisherId = bookViewPublisher.Id;

                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            return Ok(bookView);
        }



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
