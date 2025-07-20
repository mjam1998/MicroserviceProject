
using MediatR;
using Ordering.Application.Contracts.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public DeleteOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderForDelete=await _orderRepository.GetByIdAsync(request.Id);
            await _orderRepository.DeleteAsync(orderForDelete);

            return Unit.Value;
        }
    }
}
