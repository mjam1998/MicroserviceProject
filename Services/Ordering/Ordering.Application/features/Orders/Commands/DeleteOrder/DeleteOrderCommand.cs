

using MediatR;

namespace Ordering.Application.features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommand:IRequest
    {
        public int Id { get; set; }
    }
}
