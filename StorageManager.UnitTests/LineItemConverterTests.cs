using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using StorageManager.Models;
using StorageManager.Services;

namespace StorageManager.UnitTests
{
    [TestClass]
    public class LineItemConverterTests
    {
        [DataTestMethod]
        [DataRow("InputFiles\\CorrectInput.txt")]
        public async Task ConvertInputToStorageModelCollection_CorrectPayload_ReturnStorageCollection(string path)
        {
            // arrange
            var inputLines = (await FileReadeHelper.ReadFromFileAsync(path)).Split(Environment.NewLine).ToList();

            var logger = Substitute.For<ILogger<LineItemConverter>>();
            var service = new LineItemConverter(logger);

            // act
            var actual = service.ConvertToStorageModelCollection(inputLines);

            // assert
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(List<Storage>));
            Assert.AreEqual(14, actual.Count);
        }

        [DataTestMethod]
        [DataRow("InputFiles\\IncorrectInput.txt")]
        public async Task ConvertInputToStorageModelCollection_IncorrectPayload_ReturnEmptyCollection(string path)
        {
            // arrange
            var inputLines = (await FileReadeHelper.ReadFromFileAsync(path)).Split(Environment.NewLine).ToList();

            var logger = Substitute.For<ILogger<LineItemConverter>>();
            var service = new LineItemConverter(logger);

            // act
            var actual = service.ConvertToStorageModelCollection(inputLines);

            // assert
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(List<Storage>));
            Assert.AreEqual(0, actual.Count);
            logger.Received().Log(
                LogLevel.Error,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString() == "Index was outside the bounds of the array."),
                null,
                Arg.Any<Func<object, Exception, string>>());
        }
    }
}