using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StorageManager.Interfaces;

namespace StorageManager.Services
{
    public class DataLoader : IDataLoader
    {
        private readonly ILogger<DataLoader> _logger;
        private readonly IFileReader _fileReader;
        private readonly IConfiguration _configuration;
        private readonly ILineItemConverter _lineItemConverter;
        private readonly IStorageRepository _storageRepository;

        public DataLoader(ILogger<DataLoader> logger,
            IFileReader fileReader,
            IConfiguration configuration,
            ILineItemConverter lineItemConverter,
            IStorageRepository storageRepository)
        {
            _logger = logger;
            _fileReader = fileReader;
            _configuration = configuration;
            _lineItemConverter = lineItemConverter;
            _storageRepository = storageRepository;
        }

        public async Task LoadDataFromFileAsync()
        {
            try
            {
                var payload = await _fileReader.ReadFileAsLinesAsync(_configuration.FilePath);

                var storageCollection = _lineItemConverter.ConvertToStorageModelCollection(payload);

                // This method could simply return storage collection but I decide to use repository pattern
                // to store loaded items. This way app can be easy adapted to work with db.
                _storageRepository.AddCollection(storageCollection);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        }
    }
}