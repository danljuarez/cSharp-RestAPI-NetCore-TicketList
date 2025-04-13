using RESTfulNetCoreWebAPI_TicketList.Models;

namespace RESTfulNetCoreWebAPI_TicketList.Services
{
    public interface ITicketService
    {
        Task<Ticket> AddTicketAsync(Ticket? ticket);

        Task DeleteTicketAsync(int id);

        Task<Ticket?> GetTicketAsync(int id);

        Task<List<Ticket>> GetTicketsAsync();

        Task<Ticket> PatchTicketAsync(Ticket ticket);
    }
}