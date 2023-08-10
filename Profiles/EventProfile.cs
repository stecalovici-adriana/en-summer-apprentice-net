using AutoMapper;
using TicketManagerSystem.Api.Models;
using TicketManagerSystem.Api.Models.DTO;

namespace TicketManagerSystem.Api.Profiles
{
    public class EventProfile:Profile
    {
        public EventProfile()
        {
            /*CreateMap<Event, EventDTO>()
                .ForMember(
                    dest => dest.EventType,
                    opt => opt.MapFrom(src => src.EventType.EventTypeName))
                .ForMember(
                    dest => dest.Venue,
                    opt => opt.MapFrom(src => src.Venue.Location)).ReverseMap();*/

            CreateMap<Event, EventDTO>().ForMember(dest => dest.EventType, opt => opt.MapFrom(src => src.EventType.EventTypeName))
                .ForMember(dest => dest.Venue, opt => opt.MapFrom(src => src.Venue.Location)).ForMember(dest => dest.TicketCategory, opt => opt.MapFrom(src => src.TicketCategories.Where(tc => tc.EventId == src.EventId).Select(tc => new TicketCategoryDTO
                {
                    TicketCategoryId = tc.TicketCategoryId,
                    EventId = tc.EventId,
                    Description = tc.Description,
                    Price = tc.Price

                }).ToList()));

            CreateMap<Event, EventPatchDTO>().ReverseMap();
        }
    }
}
