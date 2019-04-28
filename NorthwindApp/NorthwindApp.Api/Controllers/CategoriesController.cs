using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindApp.Core.Interfaces;
using NorthwindApp.Models;
using NorthwindApp.Services.Interfaces;

namespace NorthwindApp.Api.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a service for managing categories
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMimeHelper _mimeHelper;

        /// <inheritdoc />
        public CategoriesController(
            ICategoryService categoryService,
            IMimeHelper mimeHelper)
        {
            _categoryService = categoryService;
            _mimeHelper = mimeHelper;
        }

        /// <summary>
        /// Gets a list of categories
        /// </summary>
        /// <returns>A list of all categories</returns>
        [ProducesResponseType(typeof(IEnumerable<Category>), StatusCodes.Status200OK)]
        [HttpGet(Name = "GetCategories")]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            var categories = await _categoryService.GetCategoriesAsync();

            return Ok(categories);
        }

        /// <summary>
        /// Gets an image of a category
        /// </summary>
        /// <param name="id">Category id</param>
        /// <returns>An image</returns>
        /// <response code="404">Requested image was not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpGet("images/{id}", Name = "GetCategoryImage")]
        public async Task<ActionResult> GetImage(int id)
        {
            var image = await _categoryService.GetCategoryImageAsync(id);

            if (image == null)
            {
                return NotFound();
            }

            return File(image, _mimeHelper.GetMimeType(image));
        }

        /// <summary>
        /// Uploads new image to specified category
        /// </summary>
        /// <param name="id">Category id</param>
        /// <param name="image">New Image</param>
        /// <response code="204">New image was successfully uploaded</response>
        /// <response code="400">New image should not be null</response>
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [HttpPut("images/{id}", Name = "UploadCategoryImage")]
        public async Task<ActionResult> UploadImage(int id, [FromForm] IFormFile image)
        {
            if (image  == null)
            {
                return BadRequest();
            }

            using (var memoryStream = new MemoryStream())
            {
                await image.CopyToAsync(memoryStream);
                await _categoryService.UploadCategoryImageAsync(id, memoryStream.ToArray());
            }

            return NoContent();
        }
    }
}
