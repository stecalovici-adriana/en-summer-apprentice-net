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
        private readonly ITicketCategoryRepository _ticketCategoryRepository;

        public OrderController(IOrderRepository orderRepository, IMapper mapper, ITicketCategoryRepository ticketCategoryRepository) // ILogger<EventController> logger
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _ticketCategoryRepository = ticketCategoryRepository;

        }
        [HttpGet]
        public ActionResult<List<OrderDTO>> GetAll()
        {

            var orders = _orderRepository.GetAll().ToList();
            var ordersDTO = _mapper.Map<List<OrderDTO>>(orders);
            return Ok(ordersDTO);
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
            if (orderPatch.TicketCategoryID != 0) orderEntity.TicketCategoryId = orderPatch.TicketCategoryID;
            var priceOfTicket = _ticketCategoryRepository.GetPriceByTicketCategoryId(orderPatch.TicketCategoryID);

            if (orderEntity.TotalPrice != 0) orderEntity.TotalPrice = orderPatch.NumberOfTickets * priceOfTicket;

            _orderRepository.Update(orderEntity);
            var orderEntityDto = _mapper.Map<OrderDTO>(orderEntity);

            return Ok(orderEntityDto);
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
