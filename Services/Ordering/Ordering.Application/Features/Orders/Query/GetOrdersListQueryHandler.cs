using AutoMapper;
using MediatR;
using Ordering.Domain.Contracts;
using System;
using System.Collections.Generic;

using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Query
{
    public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrdersVm>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async  Task<List<OrdersVm>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
        {
            var orderlist= await _orderRepository.GetOrdersByUserName(request.UserName);
            return _mapper.Map < List<OrdersVm>>(orderlist);      
        } 
    }
}
