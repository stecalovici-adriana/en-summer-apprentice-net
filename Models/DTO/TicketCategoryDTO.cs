namespace TicketManagerSystem.Api.Models.DTO
{
    public class TicketCategoryDTO
    {
        public int TicketCategoryId { get; set; }

        public int EventId { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }
    }
}
