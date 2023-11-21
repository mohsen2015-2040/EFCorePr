using EFCorePr.Models;
using Microsoft.EntityFrameworkCore.Internal;
using System.Runtime.CompilerServices;

namespace EFCorePr
{
    public static class MyLoggingExtention
    {
        public async static Task LogToFile(this ILogger logger, string path, string message)
        {
            if(!Directory.Exists(path))
                Directory.CreateDirectory(path);
            
            await File.WriteAllTextAsync(Path.Combine(path, $"{DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(".", "-")}.txt"), $"{DateTime.Now}: " + message);
        }

    }
}
