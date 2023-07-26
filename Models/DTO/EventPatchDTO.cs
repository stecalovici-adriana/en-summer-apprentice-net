namespace TicketManagerSystem.Api.Models.DTO
{
    public class EventPatchDTO
    {
        public int EventID { get; set; }
        public string EventName { get; set; } = string.Empty;
        public string EventDescription { get; set; }

    }
}
