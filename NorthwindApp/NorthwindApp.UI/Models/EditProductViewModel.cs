using System.Collections.Generic;

namespace NorthwindApp.UI.Models
{
    public class EditProductViewModel
    {
        public ProductViewModel Product { get; set; }

        public IEnumerable<BaseCategoryViewModel> Categories { get; set; }

        public IEnumerable<BaseSupplierViewModel> Suppliers { get; set; }
    }
}
