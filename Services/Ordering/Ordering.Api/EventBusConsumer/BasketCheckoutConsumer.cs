using AutoMapper;
using EventBus.Message.Events;
using MassTransit;
using MediatR;
using Ordering.Application.Features.Orders.Command.CheckoutOrder;
using System.Threading.Tasks;

namespace Ordering.Api.EventBusConsumer
{
    public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public BasketCheckoutConsumer(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async  Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
           var command=_mapper.Map<CheckoutOrderCommand>(context.Message);
            await _mediator.Send(command);
        }
    }
}
