namespace TicketManagerSystem.Api.Models.DTO
{
    public class OrderPatchDTO
    {
        public int OrderID { get; set; }
        public int NumberOfTickets { get; set; }
        //public decimal TotalPrice { get; set; }
        //public DateTime OrderedAt { get; set; }

        public int TicketCategoryID { get; set; }

    }
}
