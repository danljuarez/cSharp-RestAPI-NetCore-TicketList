﻿using RESTfulNetCoreWebAPI_TicketList.Models;
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
                throw new ArgumentOutOfRangeException(nameof(id), "Parameter value cannot be cero or negative.");
            }

            var ticket = _ticketRepository.GetTicket(id);
            if (ticket == null)
            {
                throw new KeyNotFoundException("Ticket was not found.");
            }

            _ticketRepository.DeleteTicket(ticket);
            await _ticketRepository.SaveAsync();
        }
    }
}
