using RESTfulNetCoreWebAPI_TicketList.Models;

namespace RESTfulNetCoreWebAPI_TicketList.Services
{
    public interface ITicketService
    {
        Task<Ticket> AddTicketAsync(Ticket? ticket);

        Task DeleteTicketAsync(int id);

        Ticket? GetTicket(int id);

        Task<List<Ticket>> GetTicketsAsync();

        Task<Ticket> PatchTicketAsync(Ticket ticket);
    }
}