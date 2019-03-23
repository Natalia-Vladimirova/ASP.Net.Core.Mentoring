using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace NorthwindApp.UI.Models
{
    public class CategoryImageDetailsViewModel
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "New image is not selected")]
        public IFormFile Picture { get; set; }
    }
}
