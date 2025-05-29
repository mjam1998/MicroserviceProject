using Catalog.Api.Entites;
using Catalog.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Threading.Tasks;

namespace Catalog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public CatalogController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet]
        public async Task<IActionResult>  GetProducts()
        {
            var result= await _productRepository.GetProducts();
            return Ok(result);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetProduct(string id)
        {
            var result=await _productRepository.GetProductById(id);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            await _productRepository.CreateProduct(product);
            return CreatedAtAction("AddProduct", new { id = product.Id });

        }
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productRepository.DeleteProduct(id);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            return Ok(await _productRepository.UpdateProduct(product));
        }  
    }
}
