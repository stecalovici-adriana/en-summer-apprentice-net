using TicketManagerSystem.Api.Models;

namespace TicketManagerSystem.Api.Repositories
{
    public class TicketCategoryRepository : ITicketCategoryRepository
    {
        private readonly TicketManagerSystemContext _dbContext;



        public TicketCategoryRepository()
        {
            _dbContext = new TicketManagerSystemContext();
        }
        public decimal GetPriceByTicketCategoryId(int id)
        {
            var ticketCategory = _dbContext.TicketCategories
                                          .Where(t => t.TicketCategoryId == id)
                                          .FirstOrDefault();





            if (ticketCategory != null)
            {
                return ticketCategory.Price;
            }



            return -1;
        }
    }
}
