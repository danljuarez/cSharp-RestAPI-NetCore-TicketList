using RESTfulNetCoreWebAPI_TicketList.Models;
using RESTfulNetCoreWebAPI_TicketList.Repositories;

namespace RESTfulNetCoreWebAPI_TicketList.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public List<Ticket> GetTickets()
        {
            return _ticketRepository.GetTickets();
        }

        public Ticket? GetTicket(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException();
            }

            return _ticketRepository.GetTicket(id);
        }

        public async Task<Ticket> AddTicketAsync(Ticket? ticket)
        {
            if (ticket == null)
            {
                throw new ArgumentNullException();
            }

            _ticketRepository.AddTicket(ticket);
            await _ticketRepository.SaveAsync();

            return ticket;
        }

        public async Task<Ticket> PatchTicketAsync(Ticket ticket)
        {
            _ticketRepository.UpdateTicket(ticket);
            await _ticketRepository.SaveAsync();

            return ticket;
        }

        public async Task DeleteTicketAsync(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException();
            }

            var ticket = _ticketRepository.GetTicket(id);
            if (ticket == null)
            {
                throw new ArgumentNullException();
            }

            _ticketRepository.DeleteTicket(ticket);
            await _ticketRepository.SaveAsync();
        }
    }
}
