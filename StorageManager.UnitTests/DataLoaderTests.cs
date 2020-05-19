using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using StorageManager.Interfaces;
using StorageManager.Models;
using StorageManager.Services;

namespace StorageManager.UnitTests
{
    [TestClass]
    public class DataLoaderTests
    {
        [TestMethod]
        public async Task LoadDataFromFileAsync_CorrectPayload_CompleteWithSuccess()
        {
            // arrange

            var path = "testFilePath.txt";
            var inputLines = new List<string>();

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

            var logger = Substitute.For<ILogger<DataLoader>>();
            var fileReader = Substitute.For<IFileReader>();
            var configuration = Substitute.For<IConfiguration>();
            var modelConverter = Substitute.For<ILineItemConverter>();
            var storageRepository = Substitute.For<IStorageRepository>();

            configuration.FilePath.Returns(path);
            fileReader.ReadFileAsLinesAsync(path).Returns(Task.FromResult(inputLines));
            modelConverter.ConvertToStorageModelCollection(inputLines).Returns(storageCollection);

            var service = new DataLoader(logger, fileReader, configuration, modelConverter, storageRepository);

            // act
            await service.LoadDataFromFileAsync();

            // assert
            storageRepository.Received().AddCollection(storageCollection);
            modelConverter.Received().ConvertToStorageModelCollection(inputLines);
            await fileReader.Received().ReadFileAsLinesAsync(path);
            Assert.AreEqual(path, configuration.FilePath);
        }
    }
}