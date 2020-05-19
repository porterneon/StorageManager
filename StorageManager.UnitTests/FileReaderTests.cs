using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StorageManager.UnitTests
{
    [TestClass]
    public class FileReaderTests
    {
        [DataTestMethod]
        [DataRow("InputFiles\\CorrectInput.txt")]
        public async Task ReadFileAsLinesAsync_CorrectFilePath_ReturnsTextLineCollection(string path)
        {
            // arrange
            var inputLines = (await FileReadeHelper.ReadFromFileAsync(path)).Split(Environment.NewLine).ToList();
            var service = new Services.FileReader();

            // act
            var actual = await service.ReadFileAsLinesAsync(path);

            // assert
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(List<string>));
            Assert.AreEqual(inputLines.Count, actual.Count);
        }

        [TestMethod]
        public async Task ReadFileAsLinesAsync_Null_ReturnsNull()
        {
            // arrange
            var service = new Services.FileReader();

            // act
            var actual = await service.ReadFileAsLinesAsync(null);

            // assert
            Assert.IsNull(actual);
        }
    }
}