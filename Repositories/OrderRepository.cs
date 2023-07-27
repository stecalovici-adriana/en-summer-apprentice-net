using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketManagerSystem.Api.Exceptions;
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

        public async Task<Order> GetById(int id)
        {
            var @order = await _dbContext.Orders.Where(e => e.OrderId == id).FirstOrDefaultAsync();
            if (@order == null)
                throw new EntityNotFoundException(id, nameof(Order));
            return @order;
        }

        public void Update(Order @order)
        {
            _dbContext.Entry(@order).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
