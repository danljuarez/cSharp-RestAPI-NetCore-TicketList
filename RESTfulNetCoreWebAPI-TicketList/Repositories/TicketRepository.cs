using Microsoft.EntityFrameworkCore;
using RESTfulNetCoreWebAPI_TicketList.Data;
using RESTfulNetCoreWebAPI_TicketList.Models;

namespace RESTfulNetCoreWebAPI_TicketList.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly TicketContext _ticketContext;

        public TicketRepository(TicketContext ticketContext)
        {
            _ticketContext = ticketContext;
        }

        public async Task<List<Ticket>> GetTicketsAsync()
        {
            return await _ticketContext.Tickets.ToListAsync();
        }

        public async Task<Ticket?> GetTicketAsync(int id)
        {
            var ticket = (await _ticketContext.Tickets.ToListAsync()).FirstOrDefault(t => t.Id == id);

            return ticket;
        }

        public Ticket AddTicket(Ticket ticket)
        {
            _ticketContext.Tickets.Add(ticket);

            return ticket;
        }

        public Ticket UpdateTicket(Ticket ticket)
        {
            _ticketContext.Tickets.Update(ticket);

            return ticket;
        }

        public void DeleteTicket(Ticket ticket)
        {
            _ticketContext.Tickets.Remove(ticket);
        }

        public async Task SaveAsync()
        {
            await _ticketContext.SaveChangesAsync();
        }
    }
}
