using Microsoft.VisualStudio.TestTools.UnitTesting;
using StorageManager.Models;
using StorageManager.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StorageManager.UnitTests
{
    [TestClass]
    public class OutputFormatterTests
    {
        [DataTestMethod]
        [DataRow("InputFiles\\ExpectedFormattedText.txt")]
        public async Task FormatOutputText_ReturnsFormattedText(string path)
        {
            // arrange
            var service = new OutputFormatter();
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
                },
                new Storage()
                {
                    Id = "testId2",
                    Materials = new List<Material>()
                    {
                        new Material()
                        {
                            Id = "materialId2",
                            Name = "materialName1",
                            Quantity = 5
                        }
                    }
                },
                new Storage()
                {
                    Id = "testId3",
                    Materials = new List<Material>()
                    {
                        new Material()
                        {
                            Id = "materialId3",
                            Name = "materialName3",
                            Quantity = 7
                        }
                    }
                }
            };


            var expected = await FileReadeHelper.ReadFromFileAsync(path);

            // act
            var actual = service.FormatOutputText(storageCollection);

            // assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }
    }
}
