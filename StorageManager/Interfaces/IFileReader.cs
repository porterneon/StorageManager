using System.Collections.Generic;
using System.Threading.Tasks;

namespace StorageManager.Interfaces
{
    public interface IFileReader
    {
        Task<List<string>> ReadFileAsLinesAsync(string path);
    }
}