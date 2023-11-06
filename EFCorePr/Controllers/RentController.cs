using EFCorePr.Controllers.Filter;
using EFCorePr.Models;
using EFCorePr.Services;
using Microsoft.AspNetCore.Mvc;

namespace EFCorePr.Controllers
{

    [Route("MyLibrary/[controller]")]
    public class RentController : Controller
    {
        private readonly BookStoreEFCoreContext _dbContext;
        private readonly IGenerateGuideToRoutsService _generateGuide;
        private readonly ILogger<RentController> _logger;

        public RentController(BookStoreEFCoreContext bookStoreContext, IGenerateGuideToRoutsService generateGuide, ILogger<RentController> logger)
        {
            _dbContext = bookStoreContext;
            _generateGuide = generateGuide;
            _logger = logger;
        }

        public ActionResult Index()
        {
            var guideMessage = _generateGuide.GenerateMessage(typeof(Rent));
            return Ok(guideMessage);
        }

        [ServiceFilter(typeof(ExceptionHandler))]
        [HttpGet]
        [Route("GetRents")]
        public IActionResult Get()
        {
            var rents = from r in _dbContext.Rent
                        join c in _dbContext.Customer
                        on r.CustomerId equals c.Id
                        join b in _dbContext.Books
                        on r.BookId equals b.Id
                        select new { r, c, b };

            string resoponse = "";
            int rentNumber = 1;

            foreach (var item in rents)
            {
                resoponse += $"[Number: {rentNumber} * Book: {item.r.Book.Title} * Reader: {item.r.Customer.LastName} " +
                    $"* Start Date: {item.r.StartDate} * Finish Date: {item.r.FinishDate} * Reader Phone: {item.r.Customer.PhoneNum}]";

                rentNumber++;
            }
            return Ok(resoponse);
        }

        [ServiceFilter(typeof(ExceptionHandler))]
        [Route("Add")]
        public IActionResult Add()
        {

            _dbContext.Rent.Add(new Models.Rent { BookId = 13, CustomerId = 1, StartDate = DateTime.Now, FinishDate = DateTime.Now.AddDays(7) });
            _dbContext.SaveChanges();

            return Ok("Successfully added.");

        }

        [ServiceFilter(typeof(ExceptionHandler))]
        [Route("Edit")]
        public IActionResult Update()
        {
            var selectedRent = _dbContext.Rent.FirstOrDefault(x => x.Id == 4);
            if (selectedRent != null)
            {
                selectedRent.FinishDate = DateTime.Now.AddDays(10);
                _dbContext.SaveChanges();

                return Ok("Successfully Updated");
            }
            else
                return Ok("Rent Not Found!");
        }
    }
}
