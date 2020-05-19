using System.IO;
using System.Threading.Tasks;

namespace StorageManager.UnitTests
{
    public static class FileReadeHelper
    {
        public static async Task<string> ReadFromFileAsync(string requestBodyPath)
        {
            var directory = Directory.GetCurrentDirectory();
            requestBodyPath = requestBodyPath.Replace('\\', Path.DirectorySeparatorChar);
            var filepath = Path.Combine(directory, requestBodyPath);

            var reader = new StreamReader(filepath);
            return await reader.ReadToEndAsync();
        }
    }
}