using RESTfulNetCoreWebAPI_TicketList.Models;

namespace RESTfulNetCoreWebAPI_TicketList.Repositories
{
    public interface ITicketRepository
    {
        Ticket AddTicket(Ticket ticket);

        void DeleteTicket(Ticket ticket);

        Ticket? GetTicket(int id);

        Task<List<Ticket>> GetTicketsAsync();

        Task SaveAsync();

        Ticket UpdateTicket(Ticket ticket);
    }
}