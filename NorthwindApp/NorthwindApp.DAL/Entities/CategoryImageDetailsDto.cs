using System.ComponentModel.DataAnnotations;

namespace NorthwindApp.DAL.Entities
{
    public class CategoryImageDetailsDto
    {
        [Key]
        public int CategoryId { get; set; }

        public CategoryDto Category { get; set; }

        public byte[] Picture { get; set; }
    }
}
