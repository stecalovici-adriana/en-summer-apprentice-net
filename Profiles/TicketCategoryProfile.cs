using AutoMapper;
using TicketManagerSystem.Api.Models.DTO;
using TicketManagerSystem.Api.Models;

namespace TMS.Api.Profiles
{
    public class TicketCategoryProfile : Profile
    {
        public TicketCategoryProfile()
        {
            CreateMap<TicketCategory, TicketCategoryDTO>().ReverseMap();
        }
    }
}