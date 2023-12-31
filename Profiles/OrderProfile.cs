﻿using AutoMapper;
using TicketManagerSystem.Api.Models.DTO;
using TicketManagerSystem.Api.Models;

namespace TicketManagerSystem.Api.Profiles
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDTO>().ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer.CustomerName)).ForMember(dest => dest.TicketCategory, opt => opt.MapFrom(src => src.TicketCategory.Description));
            CreateMap<Order, OrderPatchDTO>().ReverseMap();
        }
    }
}
