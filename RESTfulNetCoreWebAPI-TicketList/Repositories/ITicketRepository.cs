using RESTfulNetCoreWebAPI_TicketList.Models;

namespace RESTfulNetCoreWebAPI_TicketList.Repositories
{
    public interface ITicketRepository
    {
        Task<Ticket> AddTicketAsync(Ticket ticket);

        void DeleteTicket(Ticket ticket);

        Task<Ticket?> GetTicketAsync(int id);

        Task<List<Ticket>> GetTicketsAsync();

        Task SaveAsync();

        Ticket UpdateTicket(Ticket ticket);
    }
}