using StorageManager.Interfaces;
using StorageManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace StorageManager.Services
{
    public class StorageRepository : IStorageRepository
    {
        private static List<Storage> _storageCollection = new List<Storage>();
        private readonly ILogger<StorageRepository> _logger;

        public StorageRepository(ILogger<StorageRepository> logger)
        {
            _logger = logger;
        }

        public int AddCollection(List<Storage> models)
        {
            if (models == null)
            {
                _logger.LogInformation("StorageRepository: Provided input collection is null.");
                return 0;
            }

            foreach (var storage in models)
            {
                Add(storage);
            }

            return models.Count;
        }

        public int Add(Storage model)
        {
            if (model == null)
            {
                _logger.LogInformation("StorageRepository: Provided input model is null.");
                return 0;
            }

            var entity = _storageCollection.FirstOrDefault(i => i.Id == model.Id);

            if (entity == null)
                _storageCollection.Add(model);
            else
            {
                UpsertStorageItems(model, entity);
            }

            return 1;
        }

        public List<Storage> GetAll()
        {
            return _storageCollection;
        }

        private void UpsertStorageItems(Storage model, Storage entity)
        {
            foreach (var item in model.Materials)
            {
                var storageItem = entity.Materials.FirstOrDefault(i => i.Id == item.Id);
                if (storageItem == null)
                {
                    entity.Materials.Add(item);
                }
                else
                {
                    storageItem.Quantity = item.Quantity;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // This destructor will run only if the Dispose method
        // does not get called.
        // It gives the base class the opportunity to finalize.
        // Do not provide destructors in types derived from this class.
        ~StorageRepository()
        {
            Dispose(false);
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _storageCollection = null;
                }
            }
            this._disposed = true;
        }
    }
}