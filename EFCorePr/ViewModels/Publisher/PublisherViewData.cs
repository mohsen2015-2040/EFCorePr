using EFCorePr.Models;

namespace EFCorePr.ViewModels.Publisher
{
    public class PublisherViewData
    {
        public string FullName { get; set; } = string.Empty;

        public PublisherViewData() { }
        public PublisherViewData(Models.Publisher dbPublisher)
        {
            FullName = dbPublisher.FullName;
        }
    }
}
