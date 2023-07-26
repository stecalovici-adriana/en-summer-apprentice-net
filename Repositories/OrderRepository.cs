using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketManagerSystem.Api.Models;

namespace TicketManagerSystem.Api.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TicketManagerSystemContext _dbContext;
        public OrderRepository()
        {
            _dbContext = new TicketManagerSystemContext();
        }
        public int Add(Order @order)
        {
            throw new NotImplementedException();
        }

        public void Delete(Order @order)
        {
            _dbContext.Remove(@order);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Order> GetAll()
        {
            var orders = _dbContext.Orders;

            return orders;
        }

        public Task<Order> GetById(int id)
        {
            var @order = _dbContext.Orders.Where(e => e.OrderId == id).FirstOrDefaultAsync();

            return @order;
        }

        public void Update(Order @order)
        {
            _dbContext.Entry(@order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
