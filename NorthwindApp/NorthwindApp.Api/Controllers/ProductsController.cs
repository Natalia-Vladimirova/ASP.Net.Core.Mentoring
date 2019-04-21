using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NorthwindApp.Models;
using NorthwindApp.Services.Interfaces;

namespace NorthwindApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get(int page, int pageSize)
        {
            if (page < 0 || pageSize < 0)
            {
                return BadRequest();
            }

            var products = await _productService.GetProductsAsync(page, pageSize);

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = await _productService.GetProductAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Product product)
        {
            var insertedProduct = await _productService.AddProductAsync(product);

            return CreatedAtAction(nameof(Get), new { id = insertedProduct.ProductId }, insertedProduct);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            if (await _productService.GetProductAsync(id) == null)
            {
                return NotFound();
            }

            await _productService.EditProductAsync(product);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (await _productService.GetProductAsync(id) == null)
            {
                return NotFound();
            }

            await _productService.DeleteProductAsync(id);

            return NoContent();
        }
    }
}
