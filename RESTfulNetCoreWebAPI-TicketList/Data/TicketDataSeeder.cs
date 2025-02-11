using Newtonsoft.Json;
using RESTfulNetCoreWebAPI_TicketList.Models;
using System.Diagnostics.CodeAnalysis;

namespace RESTfulNetCoreWebAPI_TicketList.Data
{
    [ExcludeFromCodeCoverage]
    public class TicketDataSeeder
    {
        private readonly TicketContext _ticketContext;
        private const string TICKET_SEED_DATA_SOURCE = "resources/SeedData.json";

        public TicketDataSeeder(TicketContext ticketContext)
        {
            _ticketContext = ticketContext;
        }

        public async Task SeedData()
        {
            if (!_ticketContext.Tickets.Any())
            {
                List<Ticket> tickets = LoadTickets();
                _ticketContext.AddRange(tickets);

                await _ticketContext.SaveChangesAsync();
            }
        }

        private List<Ticket> LoadTickets()
        {
            using (FileStream fs = new FileStream(TICKET_SEED_DATA_SOURCE, FileMode.Open))
            using (StreamReader sr = new StreamReader(fs))
            using (JsonReader jr = new JsonTextReader(sr))
            {
                var jsonSerializer = new JsonSerializer();

                List<Ticket> tickets = jsonSerializer.Deserialize<List<Ticket>>(jr) ?? new List<Ticket>();

                return tickets;
            }
        }
    }
}
