using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NorthwindApp.Services.Interfaces;
using NorthwindApp.UI.Models;

namespace NorthwindApp.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var products = (await _productService.GetProductsAsync())
                .Select(_mapper.Map<ProductViewModel>);

            return View(products);
        }
    }
}
