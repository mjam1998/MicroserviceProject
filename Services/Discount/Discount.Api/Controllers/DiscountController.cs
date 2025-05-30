using Discount.Api.Entites;
using Discount.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Discount.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }
        [HttpGet("{productName}")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> GetDiscount(string productName)
        {
            var coupon=await _discountRepository.GetDiscount(productName);
            return Ok(coupon);
        }
        [HttpPost]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> CreateDiscount([FromBody] Coupon coupon)
        {
            var result=await _discountRepository.CreateDiscount(coupon);
            return CreatedAtAction("CreateDiscount", new {productName=coupon.ProductName},coupon);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateDiscount([FromBody] Coupon coupon)
        {

            return Ok(await _discountRepository.UpdateDiscount(coupon));
        }
        [HttpDelete("{productName}")]
        public async Task<IActionResult> DeleteDiscount( string productName)
        {

            return Ok(await _discountRepository.DeleteDiscount(productName));
        }
    }
}
