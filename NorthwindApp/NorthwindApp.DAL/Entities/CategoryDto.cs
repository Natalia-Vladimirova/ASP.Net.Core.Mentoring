using System.ComponentModel.DataAnnotations;

namespace NorthwindApp.DAL.Entities
{
    public class CategoryDto
    {
        [Key]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }
    }
}
