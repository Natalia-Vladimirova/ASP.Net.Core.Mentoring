using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindApp.Models;
using NorthwindApp.Services.Interfaces;

namespace NorthwindApp.Api.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a service for managing products
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        /// <inheritdoc />
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Gets a list of products
        /// </summary>
        /// <param name="page">A page of products which should be returned</param>
        /// <param name="pageSize">A number of products which should be returned. Set to 0 to get all products</param>
        /// <returns>A list of products</returns>
        /// <response code="400">Page and page size should be more or equal zero</response>
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [HttpGet(Name = "GetProducts")]
        public async Task<ActionResult<IEnumerable<Product>>> Get(int page, int pageSize)
        {
            if (page < 0 || pageSize < 0)
            {
                return BadRequest();
            }

            var products = await _productService.GetProductsAsync(page, pageSize);

            return Ok(products);
        }

        /// <summary>
        /// Gets a product by id
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>A product</returns>
        /// <response code="404">Requested product was not found</response>
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = await _productService.GetProductAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        /// <summary>
        /// Creates new product
        /// </summary>
        /// <param name="product">A product which should be created</param>
        /// <returns>A newly created product</returns>
        /// <response code="201">New product was created</response>
        [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
        [HttpPost(Name = "CreateProduct")]
        public async Task<ActionResult> Post([FromBody] Product product)
        {
            var insertedProduct = await _productService.AddProductAsync(product);

            return CreatedAtAction(nameof(Get), new { id = insertedProduct.ProductId }, insertedProduct);
        }

        /// <summary>
        /// Updates an existing product
        /// </summary>
        /// <param name="id">Id of a product to update</param>
        /// <param name="product">A product to update</param>
        /// <response code="204">A product was successfully updated</response>
        /// <response code="400">Id in route should be equal to id of a product to update</response>
        /// <response code="404">A product was not found</response>
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpPut("{id}", Name = "UpdateProduct")]
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

        /// <summary>
        /// Deletes an existing product
        /// </summary>
        /// <param name="id">Id of a product to delete</param>
        /// <response code="204">A product was successfully deleted</response>
        /// <response code="404">A product was not found</response>
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpDelete("{id}", Name = "DeleteProduct")]
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
