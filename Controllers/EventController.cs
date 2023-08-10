using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using TicketManagerSystem.Api.Exceptions;
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
        //private readonly ILogger _logger;   
        public EventController(IEventRepository eventRepository, IMapper mapper)//ILogger<EventController> logger
        {
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
            //_logger = logger;   
        }

        [HttpGet]
        public ActionResult<List<EventDTO>> GetAll()
        {
            var events = _eventRepository.GetAll();

            var eventDto = events.Select(e => _mapper.Map<EventDTO>(e));
            return Ok(eventDto);
        }



        [HttpGet]
        public async Task<ActionResult<EventDTO>> GetByEventId(int id)
        {
            try
            {
                var @event = await _eventRepository.GetByEventId(id);

                if (@event == null)
                {
                    return NoContent();
                }



                var eventDTO = _mapper.Map<EventDTO>(@event);
                //Task.Delay(TimeSpan.FromSeconds(2));
                return Ok(eventDTO);
            }catch(EntityNotFoundException ex)
            {
                return NotFound(new { ErrorMessage = ex.Message });
            }
        }

        [HttpPatch]
        public async Task<ActionResult<EventPatchDTO>> Patch(EventPatchDTO eventPatch)
        {
            if (eventPatch == null)
                throw new ArgumentNullException(nameof(eventPatch));
            var eventEntity = await _eventRepository.GetByEventId(eventPatch.EventID);

            if (eventEntity == null)
            {
                return NotFound();
            }
           // if (!eventPatch.EventName.IsNullOrEmpty()) eventEntity.EventName = eventPatch.EventName;
            //if (!eventPatch.EventDescription.IsNullOrEmpty()) eventEntity.EventDescription = eventPatch.EventDescription;
            _mapper.Map(eventPatch, eventEntity);
            _eventRepository.Update(eventEntity);
            return Ok(eventEntity);
           // return NoContent(); 
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var eventEntity = await _eventRepository.GetByEventId(id);

            if (eventEntity == null)
            {
                return NotFound();
            }
            _eventRepository.Delete(eventEntity);
            return NoContent();
        }
    }
}
