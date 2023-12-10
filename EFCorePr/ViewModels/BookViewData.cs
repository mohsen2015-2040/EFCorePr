using EFCorePr.Models;

namespace EFCorePr.ViewModels
{
    public class BookViewData
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Isbn { get; set; } = string.Empty;

        public string PublisherName {  get; set; } = string.Empty;

        public BookViewData() { }
        public BookViewData(Book dbBook)
        {
            Title = dbBook.Title;

            Description = dbBook.Description;

            PublisherName = dbBook.Publisher.FullName;

            Isbn = dbBook.Isbn;
        }
    }
}
