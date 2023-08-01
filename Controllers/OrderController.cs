using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketManagerSystem.Api.Exceptions;
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
        public OrderController(IOrderRepository orderRepository, IMapper mapper) // ILogger<EventController> logger
        {
            _orderRepository = orderRepository;
            _mapper = mapper;

        }
        [HttpGet]
        public ActionResult<List<OrderDTO>> GetAll()
        {
            var orders = _orderRepository.GetAll();

            var dtoOrders = orders.Select(o => _mapper.Map<OrderDTO>(o));
            return Ok(dtoOrders);
        }
        [HttpGet]
        public async Task<ActionResult<OrderDTO>> GetByOrderId(int id)
        {
            try
            {
                var @order = await _orderRepository.GetByOrderId(id);
                var orderDto = _mapper.Map<OrderDTO>(@order);
                return Ok(orderDto);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new { ErrorMessage = ex.Message });
         
            }
        }

        [HttpPatch]
        public async Task<ActionResult<OrderPatchDTO>> Patch(OrderPatchDTO orderPatch)
        {
            if (orderPatch == null)
                throw new ArgumentNullException(nameof(orderPatch));
            var orderEntity = await _orderRepository.GetByOrderId(orderPatch.OrderID);

            if (orderEntity == null)
            {
                return NotFound();
            }
            if(orderPatch.NumberOfTickets!=0) orderEntity.NumberOfTickets = orderPatch.NumberOfTickets;
            _orderRepository.Update(orderEntity);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var orderEntity = await _orderRepository.GetByOrderId(id);

            if (orderEntity == null)
            {
                return NotFound();
            }
            _orderRepository.Delete(orderEntity);
            return NoContent();
        }
    }
}
