using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketManagerSystem.Api.Models;
using TicketManagerSystem.Api.Models.DTO;
using TicketManagerSystem.Api.Repositories;

namespace TicketManagerSystem.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
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

            /*var dtoOrder = new OrderDTO()
            {
                OrderID = @order.OrderId,
                NumberOfTickets = @order.NumberOfTickets,
                TotalPrice = @order.TotalPrice,
                OrderedAt = @order.OrderedAt
            };*/

            var orderDTO = _mapper.Map<OrderDTO>(@order);

            return Ok(orderDTO);
        }

        [HttpPatch]
        public async Task<ActionResult<OrderPatchDTO>> Patch(OrderPatchDTO orderPatch)
        {
            var orderEntity = await _orderRepository.GetById(orderPatch.OrderID);

            if (orderEntity == null)
            {
                return NotFound();
            }
            _mapper.Map(orderPatch, orderEntity);
            _orderRepository.Update(orderEntity);
            return Ok(orderEntity);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var orderEntity = await _orderRepository.GetById(id);

            if (orderEntity == null)
            {
                return NotFound();
            }
            _orderRepository.Delete(orderEntity);
            return NoContent();
        }
    }
}
