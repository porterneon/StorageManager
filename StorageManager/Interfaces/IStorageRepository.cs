using StorageManager.Models;
using System;
using System.Collections.Generic;

namespace StorageManager.Interfaces
{
    public interface IStorageRepository : IDisposable
    {
        int AddCollection(List<Storage> models);

        int Add(Storage storage);

        List<Storage> GetAll();
    }
}