using NorthwindApp.UI.Attributes.Validation;
using System.ComponentModel.DataAnnotations;

namespace NorthwindApp.UI.Models
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }

        [Display(Name = "Product name")]
        [Required(ErrorMessage = "Product name is not specified")]
        public string ProductName { get; set; }

        [Display(Name = "Quantity Per Unit")]
        public string QuantityPerUnit { get; set; }

        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Units In Stock")]
        [GreaterThanOrEqual(0, ErrorMessage = "Units In Stock should be a positive value")]
        public short UnitsInStock { get; set; }

        [Display(Name = "Units On Order")]
        [GreaterThanOrEqual(0, ErrorMessage = "Units On Order should be a positive value")]
        public short UnitsOnOrder { get; set; }

        [Display(Name = "Reorder Level")]
        [GreaterThanOrEqual(0, ErrorMessage = "Reorder Level should be a positive value")]
        public short ReorderLevel { get; set; }

        [Display(Name = "Discontinued")]
        public bool Discontinued { get; set; }

        public BaseSupplierViewModel Supplier { get; set; }

        public BaseCategoryViewModel Category { get; set; }
    }
}
