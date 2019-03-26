using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NorthwindApp.Models;
using NorthwindApp.Services.Interfaces;
using NorthwindApp.UI.Models;
using X.PagedList;
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

        public async Task<IActionResult> Index(int page = 1)
        {
            var products = await GetPagedList(page);

            return View(products);
        }

        [HttpGet]
        public async Task<ActionResult> Add()
        {
            var model = await BuildEditModel(null);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Add(EditProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var addModel = await BuildEditModel(model.Product);

                return View(addModel);
            }

            var product = _mapper.Map<Product>(model.Product);
            await _productService.AddProductAsync(product);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id = 0)
        {
            var product = await _productService.GetProductAsync(id);

            if (product == null)
            {
                return RedirectToAction("Index");
            }

            var model = await BuildEditModel(_mapper.Map<ProductViewModel>(product));

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var editModel = await BuildEditModel(model.Product);

                return View(editModel);
            }

            var product = _mapper.Map<Product>(model.Product);
            await _productService.EditProductAsync(product);

            return RedirectToAction("Index");
        }

        private async Task<StaticPagedList<ProductViewModel>> GetPagedList(int page)
        {
            var pageSize = _configurationProvider.DefaultProductPageSize;
            var totalCount = await _productService.GetProductsCountAsync();

            var products = (await _productService.GetProductsAsync(page, pageSize))
                .Select(_mapper.Map<ProductViewModel>);

            return new StaticPagedList<ProductViewModel>(products, page, pageSize, totalCount);
        }

        private async Task<EditProductViewModel> BuildEditModel(ProductViewModel model)
        {
            return new EditProductViewModel
            {
                Product = model,
                Categories = (await _categoryService.GetCategoriesAsync()).Select(_mapper.Map<BaseCategoryViewModel>),
                Suppliers = (await _supplierService.GetSuppliersAsync()).Select(_mapper.Map<BaseSupplierViewModel>)
            };
        }
    }
}
