namespace RESTfulNetCoreWebAPI_TicketList.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public string? EventName { get; set; }

        public string? Description { get; set; }

        public DateTime? EventDate { get; set; } = DateTime.Today;

        // Computed class property
        public string? TicketNumber
        {
            get
            {
                return $"{EventDate?.ToString("yyyyMMdd")}{Id.ToString().PadLeft(5, '0')}";
            }
        }
    }
}
