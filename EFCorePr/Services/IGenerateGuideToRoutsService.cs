using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EFCorePr.Services
{
    public interface IGenerateGuideToRoutsService
    {
        public string GenerateMessage(Type entity);
    }
}
