using AutoMapper;
using Basket.Api.Entites;
using Basket.Api.GrpcServices;
using Basket.Api.Repositories;
using EventBus.Message.Events;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Basket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly DiscountGrpcService _discountGrpcService;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public BasketController(IBasketRepository basketRepository,DiscountGrpcService discountGrpcService,IMapper mapper,IPublishEndpoint publishEndpoint)
        {
            _basketRepository = basketRepository;
            _discountGrpcService = discountGrpcService;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }
        [HttpGet("{userName}")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket=await _basketRepository.GetUserBasket(userName);
            return Ok(basket ?? new ShoppingCart(userName));
        }
        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart cart)
        {
            foreach (var item in cart.Items)
            {
                var coupon = await _discountGrpcService.GetDiscounts(item.ProductName);
                item.Price -= coupon.Amount;
            }
            return CreatedAtAction("UpdateBasket", await _basketRepository.UpdateBasket(cart));
        }
        [HttpDelete("{userName}")]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await _basketRepository.DeleteBasket(userName);
            return Ok();
        }
        //ارتباط  order  از طریق  rabbitmq
        [HttpPost("[action]")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            // get existing basket with total price
            var basket = await _basketRepository.GetUserBasket(basketCheckout.UserName);
            if (basket == null)
            {
                return BadRequest();
            }


            // create BasketCheckoutEvent -- set total price on basketCheckout event message
            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMessage.TotalPrice=basket.TotalPrice;


            // send checkout event to rabbitmq
            await _publishEndpoint.Publish(eventMessage);

            // remove basket
            await _basketRepository.DeleteBasket(basketCheckout.UserName);


            return Accepted();
        }

    }
}
