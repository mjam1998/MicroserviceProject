﻿using MediatR;

using System.Collections.Generic;


namespace Ordering.Application.Features.Orders.Query
{
    public class GetOrdersListQuery:IRequest<List<OrdersVm>>
    {
        public string UserName { get; set; }

        public GetOrdersListQuery(string userName)
        {
            UserName = userName;
        }
    }
}
