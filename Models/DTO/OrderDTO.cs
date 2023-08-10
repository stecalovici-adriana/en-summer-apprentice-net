using System.Data;

namespace TicketManagerSystem.Api.Models.DTO
{
    public class OrderDTO
    {
        public int OrderID { get; set; }
        public int NumberOfTickets { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderedAt { get; set; }



        public string TicketCategory { get; set; }
        public int TicketCategoryId { get; set; }

    }
}
