using Microsoft.AspNetCore.Mvc;

namespace EFCorePr.ViewModels.Book.Update
{
    public class UpdateBookViewModel
    {
        [FromRoute]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Isbn { get; set; } = string.Empty;

        public int Price { get; set; }

        public string PublisherName { get; set; } = string.Empty;

        public string AuthorName { get; set; } = string.Empty;
    }
}
