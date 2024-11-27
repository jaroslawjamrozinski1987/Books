﻿using AutoMapper;
using Books.Core.Data.Models;
using Books.Core.DTO;

namespace Books.Core.Mapper.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Order, Order>();  
        }
    }
}
