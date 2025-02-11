namespace RESTfulNetCoreWebAPI_TicketList.Models.Request
{
    public class TicketInputDTO
    {
        public string? EventName { get; set; }

        public string? Description { get; set; }

        public DateTime? EventDate { get; set; } = DateTime.Today;
    }
}
