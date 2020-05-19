using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using StorageManager.Interfaces;

namespace StorageManager.Services
{
    public class FileReader : IFileReader
    {
        public async Task<List<string>> ReadFileAsLinesAsync(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path)) return null;

            var fileLines = new List<string>();
            using (var fileStream = File.OpenRead(path))
            using (var streamReader = new StreamReader(fileStream))
            {
                var line = await streamReader.ReadLineAsync();
                while (line != null)
                {
                    fileLines.Add(line);
                    line = await streamReader.ReadLineAsync();
                }
                return fileLines;
            }
        }
    }
}