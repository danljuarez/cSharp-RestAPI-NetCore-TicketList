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

        public async Task<List<Ticket>> GetTicketsAsync()
        {
            return await _ticketRepository.GetTicketsAsync();
        }

        public async Task<Ticket?> GetTicketAsync(int id)
        {
            if (id < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Parameter value cannot be cero or negative.");
            }

            return await _ticketRepository.GetTicketAsync(id);
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
                throw new ArgumentOutOfRangeException(nameof(id), "Parameter value cannot be cero or negative.");
            }

            var ticket = await _ticketRepository.GetTicketAsync(id);
            if (ticket == null)
            {
                throw new KeyNotFoundException("Ticket was not found.");
            }

            _ticketRepository.DeleteTicket(ticket);
            await _ticketRepository.SaveAsync();
        }
    }
}
