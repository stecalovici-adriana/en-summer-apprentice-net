using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using TicketManagerSystem.Api.Models.DTO;
using TicketManagerSystem.Api.Repositories;

namespace TicketManagerSystem.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;   
        public EventController(IEventRepository eventRepository, IMapper mapper, ILogger<EventController> logger)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _logger = logger;   
        }

        [HttpGet]
        public ActionResult<List<EventDTO>> GetAll()
        {
            var events = _eventRepository.GetAll();

            var dtoEvents = events.Select(e => _mapper.Map<EventDTO>(e));
            return Ok(dtoEvents);
        }


        [HttpGet]
        public async Task<ActionResult<EventDTO>> GetById(int id)
        {
            var @event = await _eventRepository.GetById(id);

            if (@event == null)
            {
                return NotFound();
            }



            var eventDTO = _mapper.Map<EventDTO>(@event);

            return Ok(eventDTO);
        }

        [HttpPatch]
        public async Task<ActionResult<EventPatchDTO>> Patch(EventPatchDTO eventPatch)
        {
            if (eventPatch == null)
                throw new ArgumentNullException(nameof(eventPatch));
            var eventEntity = await _eventRepository.GetById(eventPatch.EventID);

            if (eventEntity == null)
            {
                return NotFound();
            }
            _mapper.Map(eventPatch, eventEntity);
            _eventRepository.Update(eventEntity);
            return Ok(eventEntity);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var eventEntity = await _eventRepository.GetById(id);

            if (eventEntity == null)
            {
                return NotFound();
            }
            _eventRepository.Delete(eventEntity);
            return NoContent();
        }
    }
}
