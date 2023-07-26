using Microsoft.EntityFrameworkCore;
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

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAll()
        {
            var orders = _dbContext.Orders;

            return orders;
        }

        public Order GetById(int id)
        {
            var @order = _dbContext.Orders.Where(e => e.OrderId == id).FirstOrDefault();

            return @order;
        }

        public void Update(Order @order)
        {
            throw new NotImplementedException();
        }
    }
}
