using System.ComponentModel.DataAnnotations;

namespace NorthwindApp.DAL.Entities
{
    public class ProductDto
    {
        [Key]
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string QuantityPerUnit { get; set; }

        public decimal UnitPrice { get; set; }

        public short UnitsInStock { get; set; }

        public short UnitsOnOrder { get; set; }

        public short ReorderLevel { get; set; }

        public bool Discontinued { get; set; }

        public int? CategoryId { get; set; }

        public CategoryDto Category { get; set; }

        public int? SupplierId { get; set; }

        public SupplierDto Supplier { get; set; }
    }
}
