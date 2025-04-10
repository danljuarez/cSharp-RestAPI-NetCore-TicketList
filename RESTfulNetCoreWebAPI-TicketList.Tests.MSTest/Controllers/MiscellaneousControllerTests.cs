using Microsoft.AspNetCore.Mvc;
using Moq;
using RESTfulNetCoreWebAPI_TicketList.Controllers;
using RESTfulNetCoreWebAPI_TicketList.Helpers;
using RESTfulNetCoreWebAPI_TicketList.Services;
using System.Net;

namespace RESTfulNetCoreWebAPI_TicketList.Tests.MSTest.Controllers
{
    [TestClass]
    public class MiscellaneousControllerTests
    {
        private readonly Mock<IMiscellaneousService> _miscellaneousService = new();
        private readonly FibonacciSequence _fibonacciHelper = new();
        private readonly PalindromeWords _palindromeWords = new();

        [TestMethod]
        public void GetFibonacciSequence_Should_return_statusCode_Ok_confirm_count_and_returned_values_are_as_expected()
        {
            // Arrange
            var maxFibonacciValue = 8;
            var expectedFibonacciSequence = _fibonacciHelper.CalculateFibonacci(maxFibonacciValue);
            var fiboSequenceSample = Data.DataFactory.GetFirstSinglesFibonacciSequence();

            _miscellaneousService
                .Setup(_ => _.GetFibonacciSequence(It.IsAny<int>()))
                .Returns(expectedFibonacciSequence);

            var miscellaneousController = new MiscellaneousController(_miscellaneousService.Object);

            // Act
            var actionResult = miscellaneousController.GetFibonacciSequence(It.IsAny<int>());
            var result = (OkObjectResult)actionResult;
            var fiboList = (List<int>?)result?.Value;

            // Assert
            Assert.AreEqual((int)HttpStatusCode.OK, result?.StatusCode);
            Assert.AreEqual(fiboSequenceSample.Count, fiboList?.Count);
            for (int i = 0; i < fiboSequenceSample.Count; i++)
            {
                Assert.AreEqual(fiboSequenceSample[i], fiboList?[i]);
            }
        }

        [TestMethod]
        public void GetFibonacciSequence_Should_return_statusCode_Ok_confirm_count_returned_is_one()
        {
            // Arrange
            var maxFibonacciValue = 0;
            var expectedFibonacciSequence = _fibonacciHelper.CalculateFibonacci(maxFibonacciValue);

            _miscellaneousService
                .Setup(_ => _.GetFibonacciSequence(It.IsAny<int>()))
                .Returns(expectedFibonacciSequence);

            var miscellaneousController = new MiscellaneousController(_miscellaneousService.Object);

            // Act
            var actionResult = miscellaneousController.GetFibonacciSequence(maxFibonacciValue);
            var result = (OkObjectResult)actionResult;
            var fiboList = (List<int>?)result?.Value;

            // Assert
            Assert.AreEqual((int)HttpStatusCode.OK, result?.StatusCode);
            Assert.AreEqual(1, fiboList?.Count);
        }

        [TestMethod]
        public void GetFibonacciSequence_Should_return_statusCode_BadRequest_When_ArgumentOutOfRangeException_is_thrown()
        {
            // Arrange
            _miscellaneousService
                .Setup(_ => _.GetFibonacciSequence(It.IsAny<int>()))
                .Throws<ArgumentOutOfRangeException>();

            var miscellaneousController = new MiscellaneousController(_miscellaneousService.Object);

            // Act
            var actionResult = miscellaneousController.GetFibonacciSequence(-1);
            var result = (ObjectResult)actionResult;

            // Assert
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result?.StatusCode);
            Assert.IsNotNull(result?.Value);
        }

        [TestMethod]
        public void GetPalindromeWords_Should_return_statusCode_Ok_confirm_count_and_returned_values_are_as_expected()
        {
            // Arrange
            var expectedPalindromeResponse = _palindromeWords.GetPalindromeWords(Data.DataFactory.GetPalindromeRequestList());
            var palindromeResponse = Data.DataFactory.GetPalindromeResponseList();

            _miscellaneousService
                .Setup(_ => _.GetPalindromeWords(It.IsAny<string[]>()))
                .Returns(expectedPalindromeResponse);

            var miscellaneousController = new MiscellaneousController(_miscellaneousService.Object);

            // Act
            var actionResult = miscellaneousController.GetPalindromeWords(It.IsAny<string[]>());
            var result = (OkObjectResult)actionResult;
            var palindromeList = (List<string>?)result.Value;

            // Assert
            Assert.AreEqual((int)HttpStatusCode.OK, result?.StatusCode);
            Assert.AreEqual(palindromeResponse.Count, palindromeList?.Count);
            for (int i = 0; i < palindromeResponse.Count; i++)
            {
                Assert.AreEqual(palindromeResponse[i], palindromeList?[i]);
            }
        }

        [TestMethod]
        public void GetPalindromeWords_Should_confirm_result_is_not_null_and_exception_message_When_Exception_is_thrown()
        {
            // Arrange
            _miscellaneousService
                .Setup(_ => _.GetPalindromeWords(It.IsAny<string[]>()))
                .Throws<Exception>();

            var miscellaneousController = new MiscellaneousController(_miscellaneousService.Object);

            // Act
            var actionResult = miscellaneousController.GetPalindromeWords(It.IsAny<string[]>());
            var result = (ObjectResult)actionResult;
            var validationDetails = (ValidationProblemDetails?)result.Value;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Exception of type 'System.Exception' was thrown.", validationDetails?.Errors["Description"].FirstOrDefault());
        }
    }
}
