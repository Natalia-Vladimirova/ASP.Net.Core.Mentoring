using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NorthwindApp.Models;
using NorthwindApp.Services.Interfaces;
using NorthwindApp.UI.Models;
using IConfigurationProvider = NorthwindApp.Core.Interfaces.IConfigurationProvider;

namespace NorthwindApp.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;
        private readonly IConfigurationProvider _configurationProvider;

        public ProductController(
            IProductService productService,
            ICategoryService categoryService,
            ISupplierService supplierService,
            IMapper mapper,
            IConfigurationProvider configurationProvider)
        {
            _productService = productService;
            _categoryService = categoryService;
            _supplierService = supplierService;
            _mapper = mapper;
            _configurationProvider = configurationProvider;
        }

        public async Task<IActionResult> Index()
        {
            var productsCount = _configurationProvider.ProductPageSize;

            var products = (await _productService.GetProductsAsync(productsCount))
                .Select(_mapper.Map<ProductViewModel>);

            return View(products);
        }

        [HttpGet]
        public async Task<ActionResult> Add()
        {
            AddProductViewModel model = new AddProductViewModel
            {
                Categories = (await _categoryService.GetCategoriesAsync()).Select(_mapper.Map<BaseCategoryViewModel>),
                Suppliers = (await _supplierService.GetSuppliersAsync()).Select(_mapper.Map<BaseSupplierViewModel>)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Add(AddProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = (await _categoryService.GetCategoriesAsync()).Select(_mapper.Map<BaseCategoryViewModel>);
                model.Suppliers = (await _supplierService.GetSuppliersAsync()).Select(_mapper.Map<BaseSupplierViewModel>);

                return View(model);
            }

            var product = _mapper.Map<Product>(model.Product);
            await _productService.AddProductAsync(product);

            return RedirectToAction("Index");
        }
    }
}
