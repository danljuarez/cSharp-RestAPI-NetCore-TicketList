
namespace RESTfulNetCoreWebAPI_TicketList.Services
{
    public interface IMiscellaneousService
    {
        List<int> GetFibonacciSequence(int maxFibonacci);

        List<string> GetPalindromeWords(string[]? words);
    }
}