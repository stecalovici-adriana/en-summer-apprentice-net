﻿using AutoMapper;
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
        private readonly ILogger _logger;
        public OrderController(IOrderRepository orderRepository, IMapper mapper, ILogger<EventController> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = logger;
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
        public async Task<ActionResult<OrderDTO>> GetById(int id)
        {
            var @order = await _orderRepository.GetById(id);

            if (@order == null)
            {
                return NotFound();
            }



            var orderDTO = _mapper.Map<OrderDTO>(@order);

            return Ok(orderDTO);
        }

        [HttpPatch]
        public async Task<ActionResult<OrderPatchDTO>> Patch(OrderPatchDTO orderPatch)
        {
            if (orderPatch == null)
                throw new ArgumentNullException(nameof(orderPatch));
            var orderEntity = await _orderRepository.GetById(orderPatch.OrderID);

            if (orderEntity == null)
            {
                return NotFound();
            }
            _mapper.Map(orderPatch, orderEntity);
            //if(orderPatch.NumberOfTickets!=0) orderEntity.NumberOfTickets = orderPatch.NumberOfTickets;
            _orderRepository.Update(orderEntity);
            //return NoContent();
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
