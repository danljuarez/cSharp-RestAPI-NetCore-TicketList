using Microsoft.EntityFrameworkCore;
using RESTfulNetCoreWebAPI_TicketList.Models;

namespace RESTfulNetCoreWebAPI_TicketList.Data
{
    public class TicketContext : DbContext
    {
        public TicketContext(DbContextOptions<TicketContext> options) : base(options) { }

        public virtual DbSet<Ticket> Tickets { get; set; }
    }
}
