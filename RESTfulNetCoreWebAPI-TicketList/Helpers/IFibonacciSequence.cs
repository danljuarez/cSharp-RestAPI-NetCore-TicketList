
namespace RESTfulNetCoreWebAPI_TicketList.Helpers
{
    public interface IFibonacciSequence
    {
        List<int> CalculateFibonacci(int maxFibonacci = 60);
    }
}