using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using StorageManager.Interfaces;
using StorageManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StorageManager.UnitTests
{
    [TestClass]
    public class StartupTests
    {
        [TestMethod]
        public async Task RunAsync_CompleteWithSucces()
        {
            // arrange
            var logger = Substitute.For<ILogger<Startup>>();
            var dataLoader = Substitute.For<IDataLoader>();
            var dataPresenter = Substitute.For<IDataPresenter>();
            var storageRepository = Substitute.For<IStorageRepository>();
            var outputFormatter = Substitute.For<IOutputFormatter>();

            var storageCollection = new List<Storage>()
            {
                new Storage()
                {
                    Id = "testId1",
                    Materials = new List<Material>()
                    {
                        new Material()
                        {
                            Id = "materialId1",
                            Name = "materialName1",
                            Quantity = 12
                        }
                    }
                }
            };

            var formattedResponse = "test response";

            storageRepository.GetAll().Returns(storageCollection);
            outputFormatter.FormatOutputText(storageCollection).Returns(formattedResponse);
            
            var service = new Startup(logger, dataLoader, dataPresenter, storageRepository, outputFormatter);

            // act
            await service.RunAsync();

            // assert
            await dataLoader.Received().LoadDataFromFileAsync();
            storageRepository.Received().GetAll();
            outputFormatter.Received().FormatOutputText(storageCollection);
            dataPresenter.Received().PrintFormattedText(formattedResponse);

        }
    }
}
