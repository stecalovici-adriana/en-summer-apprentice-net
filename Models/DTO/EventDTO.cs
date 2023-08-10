namespace TicketManagerSystem.Api.Models.DTO
{
    public class EventDTO
    {
        public int EventID { get; set; }
        public string EventName { get; set; } = string.Empty;
        public string EventDescription { get; set; }
        public string EventType { get; set; } = string.Empty;
        public string Venue { get; set; }

        public List<TicketCategoryDTO> TicketCategory { get; set; }
    }
}
