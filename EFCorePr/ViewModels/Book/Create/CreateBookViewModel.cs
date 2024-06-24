namespace EFCorePr.ViewModels.Book.Create
{
    public class CreateBookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Isbn { get; set; } = string.Empty;

        public string PublisherName { get; set; } = string.Empty;

        public string AuthorFName { get; set; } = string.Empty;
        public string AuthorLName { get; set; } = string.Empty;
        public int Price { get; set; }

    }
}
