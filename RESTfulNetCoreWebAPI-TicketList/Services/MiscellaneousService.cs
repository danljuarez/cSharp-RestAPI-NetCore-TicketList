using RESTfulNetCoreWebAPI_TicketList.Helpers;

namespace RESTfulNetCoreWebAPI_TicketList.Services
{
    public class MiscellaneousService : IMiscellaneousService
    {
        private readonly IFibonacciSequence _fibonacciSequence;
        private readonly IPalindromeWords _palindromeWords;

        public MiscellaneousService(IFibonacciSequence fibonacciSequence, IPalindromeWords palindromeWords)
        {
            _fibonacciSequence = fibonacciSequence;
            _palindromeWords = palindromeWords;
        }

        public List<int> FibonacciList(int maxFibonacci)
        {
            return _fibonacciSequence.CalculateFibonacci(maxFibonacci);
        }

        public List<string> GetPalindromeWords(string[]? words)
        {
            try
            {
                return _palindromeWords.GetPalindromeWords(words);
            }
            catch (Exception)
            {
                // Bubble up exception
                throw;
            }
        }
    }
}
