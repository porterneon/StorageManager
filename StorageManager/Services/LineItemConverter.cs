using Microsoft.Extensions.Logging;
using StorageManager.Interfaces;
using StorageManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorageManager.Services
{
    public class LineItemConverter : ILineItemConverter
    {
        private readonly ILogger<LineItemConverter> _logger;

        public LineItemConverter(ILogger<LineItemConverter> logger)
        {
            _logger = logger;
        }

        public List<Storage> ConvertToStorageModelCollection(List<string> payload)
        {
            var storageCollection = new List<Storage>();

            if (payload == null) return storageCollection;

            try
            {
                var lines = payload.Where(i => !i.StartsWith('#')).ToList();

                var inputModels = lines.Select(LineItem.FromText);

                foreach (var inputModel in inputModels)
                {
                    storageCollection.AddRange(ConvertModelToStorageDetails(inputModel));
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return storageCollection;
        }

        private List<Storage> ConvertModelToStorageDetails(LineItem model)
        {
            var storageModels = new List<Storage>();
            foreach (var item in model.StorageQuantity)
            {
                var storageModel = new Storage()
                {
                    Id = item.Key,
                    Materials = new List<Material>()
                    {
                        new Material()
                        {
                            Id = model.MaterialId,
                            Name = model.MaterialName,
                            Quantity = item.Value
                        }
                    }
                };

                storageModels.Add(storageModel);
            }

            return storageModels;
        }
    }
}