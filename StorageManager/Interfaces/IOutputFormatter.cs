using System.Collections.Generic;
using StorageManager.Models;

namespace StorageManager.Interfaces
{
    public interface IOutputFormatter
    {
        string FormatOutputText(List<Storage> storageCollection);
    }
}