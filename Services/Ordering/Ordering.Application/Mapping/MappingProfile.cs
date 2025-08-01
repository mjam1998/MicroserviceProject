﻿

using AutoMapper;
using Ordering.Application.Features.Orders.Command.CheckoutOrder;
using Ordering.Application.Features.Orders.Query;
using Ordering.Domain.Entities;

namespace Ordering.Application.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Order,OrdersVm>().ReverseMap();
            CreateMap<Order,CheckoutOrderCommand>().ReverseMap();
        }
    }
}
