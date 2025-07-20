

using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderForUpdate=await _orderRepository.GetByIdAsync(request.Id);
            _mapper.Map(request, orderForUpdate, typeof(UpdateOrderCommand), typeof(Order));
            await _orderRepository.UpdateAsync(orderForUpdate);
            return Unit.Value;
        }
    }
}
