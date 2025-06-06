﻿using AutoMapper;
using Ordering.Application.features.Orders.Queries.GetOrdersList;
using Ordering.Domain.Entities;


namespace Ordering.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order,OrdersVm>().ReverseMap();
        }
    }
}
