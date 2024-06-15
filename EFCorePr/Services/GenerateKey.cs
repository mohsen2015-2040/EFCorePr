using System.Security.Cryptography;

namespace EFCorePr.Services
{
    public class GenerateKey
    {
        public string Generate()
        {
            var key = new Byte[32];

            RandomNumberGenerator.Create().GetBytes(key);

            return Convert.ToBase64String(key);
        }
    }
}
