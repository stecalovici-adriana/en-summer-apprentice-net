using AutoMapper;
using TicketManagerSystem.Api.Models.DTO;
using TicketManagerSystem.Api.Models;

namespace TicketManagerSystem.Api.Profiles
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Order, OrderPatchDTO>().ReverseMap();
        }
    }
}
