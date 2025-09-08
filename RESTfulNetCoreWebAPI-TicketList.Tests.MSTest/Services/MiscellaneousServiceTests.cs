using RESTfulNetCoreWebAPI_TicketList.Helpers;
using RESTfulNetCoreWebAPI_TicketList.Services;

namespace RESTfulNetCoreWebAPI_TicketList.Tests.MSTest.Services
{
    [TestClass]
    public class MiscellaneousServiceTests
    {
        private readonly FibonacciSequence _fibonacciSequence = new();
        private readonly PalindromeWords _palindromeWords = new();

        [TestMethod]
        public void FibonacciList_Should_confirm_result_is_not_null_and_returned_values_are_as_expected()
        {
            // Arrange
            var maxFibonacciValue = 8;
            var expectedFiboSequence = Data.DataFactory.GetFirstSinglesFibonacciSequence();
            var miscellaneousService = new MiscellaneousService(_fibonacciSequence, _palindromeWords);

            // Act
            var result = miscellaneousService.GetFibonacciSequence(maxFibonacciValue);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedFiboSequence.Count, result.Count);
            for (int i = 0; i < expectedFiboSequence.Count; i++)
            {
                Assert.AreEqual(expectedFiboSequence[i], result[i]);
            }
        }

        [TestMethod]
        public void FibonacciList_Should_throw_ArgumentOutOfRangeException_When_maxFibonacci_argument_is_minus_than_zero()
        {
            // Arrange
            var miscellaneousService = new MiscellaneousService(_fibonacciSequence, _palindromeWords);

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => miscellaneousService.GetFibonacciSequence(-1));
        }

        [TestMethod]
        public void GetPalindromeWords_Should_confirm_result_is_not_null_and_returned_values_are_as_expected()
        {
            // Arrange
            var miscellaneousService = new MiscellaneousService(_fibonacciSequence, _palindromeWords);
            var expectedPalindromeResult = Data.DataFactory.GetPalindromeResponseList();

            // Act
            var result = miscellaneousService.GetPalindromeWords(Data.DataFactory.GetPalindromeRequestList());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedPalindromeResult.Count, result.Count);
            for (int i = 0; i < expectedPalindromeResult.Count; i++)
            {
                Assert.AreEqual(expectedPalindromeResult[i], result[i]);
            }
        }

        [TestMethod]
        public void GetPalindromeWords_Should_throw_ArgumentNullException_When_at_least_one_item_of_the_list_is_null()
        {
            // Arrange
            var miscellaneousService = new MiscellaneousService(_fibonacciSequence, _palindromeWords);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => miscellaneousService.GetPalindromeWords(Data.DataFactory.GetAtLeastOnePalindromeItemWithNullValue()!));
        }

        [TestMethod]
        public void GetPalindromeWords_Should_throw_ArgumentNullException_When_list_has_at_least_one_Palindrome_item_with_empty_value()
        {
            // Arrange
            var miscellaneousService = new MiscellaneousService(_fibonacciSequence, _palindromeWords);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => miscellaneousService.GetPalindromeWords(Data.DataFactory.GetAtLeastOnePalindromeItemWithEmptyValue()));
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow([])]
        public void GetPalindromeWords_Should_throw_ArgumentNullException_When_list_is_null_or_empty(string[]? testCase)
        {
            // Arrange
            var miscellaneousService = new MiscellaneousService(_fibonacciSequence, _palindromeWords);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => miscellaneousService.GetPalindromeWords(testCase));
        }

        [TestMethod]
        public void GetPalindromeWords_Should_throw_ArgumentException_When_list_has_at_least_one_Palindrome_item_with_only_one_char()
        {
            // Arrange
            var miscellaneousService = new MiscellaneousService(_fibonacciSequence, _palindromeWords);

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => miscellaneousService.GetPalindromeWords(Data.DataFactory.GetAtLeastOnePalindromeItemWithOnlyOneChar()));
        }

        [TestMethod]
        public void GetPalindromeWords_Should_throw_ArgumentException_When_list_has_at_least_one_Palindrome_item_with_a_number()
        {
            // Arrange
            var miscellaneousService = new MiscellaneousService(_fibonacciSequence, _palindromeWords);

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => miscellaneousService.GetPalindromeWords(Data.DataFactory.GetAtLeastOnePalindromeItemWithOnlyANumber()));
        }

    }
}
