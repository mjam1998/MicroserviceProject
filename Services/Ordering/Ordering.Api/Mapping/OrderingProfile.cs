﻿using AutoMapper;
using EventBus.Message.Events;
using Ordering.Application.Features.Orders.Command.CheckoutOrder;

namespace Ordering.Api.Mapping
{
    public class OrderingProfile:Profile
    {
        public OrderingProfile()
        {
            CreateMap<CheckoutOrderCommand,BasketCheckoutEvent>().ReverseMap();
        }
    }
}
