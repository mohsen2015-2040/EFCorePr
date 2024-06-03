using Microsoft.AspNetCore.Mvc;

namespace EFCorePr.ViewModels.Publisher.Update
{
    public class UpdatePublisherViewmodel
    {
        [FromRoute]
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
    }
}
