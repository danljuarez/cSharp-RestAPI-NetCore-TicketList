
namespace RESTfulNetCoreWebAPI_TicketList.Services
{
    public interface IMiscellaneousService
    {
        List<int> FibonacciList(int maxFibonacci);

        List<string> GetPalindromeWords(string[]? words);
    }
}