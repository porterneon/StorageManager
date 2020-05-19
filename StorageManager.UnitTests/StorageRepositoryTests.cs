using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using StorageManager.Models;
using StorageManager.Services;

namespace StorageManager.UnitTests
{
    [TestClass]
    public class StorageRepositoryTests
    {
        [TestMethod]
        public void GetAll_ReturnStorageCollection()
        {
            // arrange
            var logger = Substitute.For<ILogger<StorageRepository>>();
            var repository = new StorageRepository(logger);
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

            repository.AddCollection(storageCollection);

            // act
            var actual = repository.GetAll();

            // assert
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(List<Storage>));
            Assert.AreEqual(storageCollection.Count, actual.Count);
        }

        [TestMethod]
        public void Add_CorrectPayload_AddItemToStorage()
        {
            // arrange
            var logger = Substitute.For<ILogger<StorageRepository>>();
            var repository = new StorageRepository(logger);

            var storage = new Storage()
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
            };

            // act
            var actual = repository.Add(storage);

            // assert
            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void Add_Null_LogInfo()
        {
            // arrange
            var logger = Substitute.For<ILogger<StorageRepository>>();
            var repository = new StorageRepository(logger);

            // act
            var actual = repository.Add(null);

            // assert
            Assert.AreEqual(0, actual);
            logger.Received().Log(
                LogLevel.Information,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString() == "StorageRepository: Provided input model is null."),
                null,
                Arg.Any<Func<object, Exception, string>>());
        }

        [TestMethod]
        public void AddCollection_CorrectPayload_AddItemCollectionToStorage()
        {
            // arrange
            var logger = Substitute.For<ILogger<StorageRepository>>();
            var repository = new StorageRepository(logger);

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

            // act
            var actual = repository.AddCollection(storageCollection);

            // assert
            Assert.AreEqual(storageCollection.Count, actual);
        }

        [TestMethod]
        public void AddCollection_Null_AddItemCollectionToStorage()
        {
            // arrange
            var logger = Substitute.For<ILogger<StorageRepository>>();
            var repository = new StorageRepository(logger);

            // act
            var actual = repository.AddCollection(null);

            // assert
            Assert.AreEqual(0, actual);
            logger.Received().Log(
                LogLevel.Information,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString() == "StorageRepository: Provided input collection is null."),
                null,
                Arg.Any<Func<object, Exception, string>>());
        }
    }
}