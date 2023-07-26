using AutoMapper;
using TicketManagerSystem.Api.Models;
using TicketManagerSystem.Api.Models.DTO;

namespace TicketManagerSystem.Api.Profiles
{
    public class EventProfile:Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventDTO>().ReverseMap();
            CreateMap<Event, EventPatchDTO>().ReverseMap();
        }
    }
}
