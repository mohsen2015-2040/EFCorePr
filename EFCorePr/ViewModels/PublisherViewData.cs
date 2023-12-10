using EFCorePr.Models;

namespace EFCorePr.ViewModels
{
    public class PublisherViewData
    {
        public string FullName { get; set; } = string.Empty;

        public PublisherViewData() { }
        public PublisherViewData(Publisher dbPublisher)
        {
            FullName = dbPublisher.FullName;
        }
    }
}
