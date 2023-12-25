namespace EFCorePr.ViewModels.Book
{
    public class UpdateBookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Isbn { get; set; } = string.Empty;
        public string PublisherName { get; set; } = string.Empty;
    }
}
