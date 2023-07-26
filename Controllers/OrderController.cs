using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketManagerSystem.Api.Models.DTO;
using TicketManagerSystem.Api.Repositories;

namespace TicketManagerSystem.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        [HttpGet]
        public ActionResult<List<OrderDTO>> GetAll()
        {
            var orders = _orderRepository.GetAll();

            var dtoOrders = orders.Select(o => new OrderDTO()
            {
                OrderID = o.OrderId,
                NumberOfTickets = o.NumberOfTickets,
                TotalPrice = o.TotalPrice,
                OrderedAt = o.OrderedAt
           
            });


            return Ok(dtoOrders);
        }
        [HttpGet]
        public ActionResult<OrderDTO> GetById(int id)
        {
            var @order = _orderRepository.GetById(id);

            if (@order == null)
            {
                return NotFound();
            }

            var dtoOrder = new OrderDTO()
            {
                OrderID = @order.OrderId,
                NumberOfTickets = @order.NumberOfTickets,
                TotalPrice = @order.TotalPrice,
                OrderedAt = @order.OrderedAt
            };

            return Ok(dtoOrder);
        }
    }
}
