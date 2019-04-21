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
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMimeHelper _mimeHelper;

        public CategoriesController(
            ICategoryService categoryService,
            IMimeHelper mimeHelper)
        {
            _categoryService = categoryService;
            _mimeHelper = mimeHelper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            var categories = await _categoryService.GetCategoriesAsync();

            return Ok(categories);
        }

        [HttpGet("images/{id}")]
        public async Task<ActionResult> GetImage(int id)
        {
            var image = await _categoryService.GetCategoryImageAsync(id);

            if (image == null)
            {
                return NotFound();
            }

            return File(image, _mimeHelper.GetMimeType(image));
        }

        [HttpPut("images/{id}")]
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
