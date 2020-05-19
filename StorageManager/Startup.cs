using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StorageManager.Interfaces;

namespace StorageManager
{
    public class Startup : IStartup
    {
        private readonly ILogger<Startup> _logger;
        private readonly IDataLoader _dataLoader;
        private readonly IDataPresenter _dataPresenter;
        private readonly IStorageRepository _storageRepository;
        private readonly IOutputFormatter _outputFormatter;

        public Startup(ILogger<Startup> logger,
            IDataLoader dataLoader,
            IDataPresenter dataPresenter,
            IStorageRepository storageRepository,
            IOutputFormatter outputFormatter)
        {
            _logger = logger;
            _dataLoader = dataLoader;
            _dataPresenter = dataPresenter;
            _storageRepository = storageRepository;
            _outputFormatter = outputFormatter;
        }

        public async Task RunAsync()
        {
            try
            {
                await _dataLoader.LoadDataFromFileAsync();

                var storageCollection = _storageRepository.GetAll();

                var output = _outputFormatter.FormatOutputText(storageCollection);

                _dataPresenter.PrintFormattedText(output);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        }
    }
}