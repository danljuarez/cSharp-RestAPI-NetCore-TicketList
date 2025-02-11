
namespace RESTfulNetCoreWebAPI_TicketList.Helpers
{
    public interface IPalindromeWords
    {
        List<string> GetPalindromeWords(string[]? words);
    }
}