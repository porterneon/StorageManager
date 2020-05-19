using StorageManager.Models;
using System.Collections.Generic;

namespace StorageManager.Interfaces
{
    public interface ILineItemConverter
    {
        List<Storage> ConvertToStorageModelCollection(List<string> payload);
    }
}