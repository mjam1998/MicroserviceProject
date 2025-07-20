using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.features.Orders.Commands.CheckoutOrder;
using Ordering.Application.features.Orders.Commands.DeleteOrder;
using Ordering.Application.features.Orders.Commands.UpdateOrder;
using Ordering.Application.features.Orders.Queries.GetOrdersList;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Ordering.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userName}",Name ="GetOrders")]
        [ProducesResponseType(typeof(IEnumerable<OrdersVm>),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable< OrdersVm>>> GetOrderByUserName(string userName)
        {
            var query= new GetOrdersListQuery(userName);
            var orderList=await _mediator.Send(query);
            return Ok(orderList);
        }

        [HttpPost(Name = "CheckoutOrder")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CheckoutOrder([FromBody] CheckoutOrderCommand command)
        {
            var result=await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut(Name ="UpdateOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateOrder(UpdateOrderCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}",Name = "DeleteOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            await _mediator.Send(new DeleteOrderCommand { Id=id});
            return NoContent();
        }

    }
}
