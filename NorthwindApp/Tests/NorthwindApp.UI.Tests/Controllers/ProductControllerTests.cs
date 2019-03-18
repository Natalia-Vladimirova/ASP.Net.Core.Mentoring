using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NorthwindApp.Models;
using NorthwindApp.Services.Interfaces;
using NorthwindApp.UI.Controllers;
using NorthwindApp.UI.Models;
using Xunit;
using IConfigurationProvider = NorthwindApp.Core.Interfaces.IConfigurationProvider;

namespace NorthwindApp.UI.Tests.Controllers
{
    public class ProductControllerTests
    {
        private readonly ProductController _productController;

        private readonly Mock<IProductService> _productServiceMock;
        private readonly Mock<ICategoryService> _categoryServiceMock;
        private readonly Mock<ISupplierService> _supplierServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IConfigurationProvider> _configurationProviderMock;

        private const int ProductsCount = 66;
        private const int NotFoundProductId = 13;
        private const int ProductId = 5;
        private const string ProductName = "Chocolate Bars";

        public ProductControllerTests()
        {
            _productServiceMock = new Mock<IProductService>();
            _productServiceMock.Setup(x => x.GetProductsAsync(It.IsAny<int>()))
                .ReturnsAsync(new[] { new Product { ProductId = ProductId, ProductName = ProductName } });
            _productServiceMock.Setup(x => x.GetProductAsync(NotFoundProductId))
                .ReturnsAsync((Product)null);
            _productServiceMock.Setup(x => x.GetProductAsync(ProductId))
                .ReturnsAsync(new Product { ProductId = ProductId, ProductName = ProductName });

            _categoryServiceMock = new Mock<ICategoryService>();
            _supplierServiceMock = new Mock<ISupplierService>();

            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(x => x.Map<ProductViewModel>(It.IsAny<Product>()))
                .Returns((Product product) => new ProductViewModel
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName
                });
            _mapperMock.Setup(x => x.Map<Product>(It.IsAny<ProductViewModel>()))
                .Returns((ProductViewModel product) => new Product
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName
                });

            _configurationProviderMock = new Mock<IConfigurationProvider>();
            _configurationProviderMock.Setup(x => x.ProductPageSize)
                .Returns(ProductsCount);

            _productController = new ProductController(
                _productServiceMock.Object,
                _categoryServiceMock.Object,
                _supplierServiceMock.Object,
                _mapperMock.Object,
                _configurationProviderMock.Object);
        }

        [Fact]
        public async Task Test_Index_ShouldReturnProducts()
        {
            var result = (await _productController.Index()) as ViewResult;
            var model = result?.Model as IEnumerable<ProductViewModel>;

            Assert.NotNull(model);
            var products = model.ToArray();

            Assert.Single(products);
            Assert.Equal(ProductId, products[0].ProductId);
            Assert.Equal(ProductName, products[0].ProductName);

            _configurationProviderMock.Verify(x => x.ProductPageSize, Times.Once);
            _productServiceMock.Verify(x => x.GetProductsAsync(ProductsCount), Times.Once);
            _mapperMock.Verify(x => x.Map<ProductViewModel>(It.Is<Product>(y => y.ProductId == ProductId)), Times.Once);
        }

        [Fact]
        public async Task Test_Add_Get_ShouldReturnModelWithEmptyProduct()
        {
            var result = (await _productController.Add()) as ViewResult;
            var model = result?.Model as EditProductViewModel;

            Assert.NotNull(model);
            Assert.Null(model.Product);

            _categoryServiceMock.Verify(x => x.GetCategoriesAsync(), Times.Once);
            _supplierServiceMock.Verify(x => x.GetSuppliersAsync(), Times.Once);
        }

        [Fact]
        public async Task Test_Add_Post_ModelIsValid_ShouldAddProduct()
        {
            var editModel = new EditProductViewModel
            {
                Product = new ProductViewModel { ProductId = ProductId }
            };
            var result = (await _productController.Add(editModel)) as RedirectToActionResult;

            Assert.NotNull(result);

            _productServiceMock.Verify(x => x.AddProductAsync(It.Is<Product>(y => y.ProductId == ProductId)), Times.Once);
        }

        [Fact]
        public async Task Test_Add_Post_ModelHasErrors_ShouldNotAddProduct()
        {
            var editModel = new EditProductViewModel
            {
                Product = new ProductViewModel { ProductId = ProductId }
            };

            _productController.ModelState.AddModelError("ProductName", "Some Error");

            var result = (await _productController.Add(editModel)) as ViewResult;
            var model = result?.Model as EditProductViewModel;

            Assert.NotNull(model);
            Assert.NotNull(model.Product);
            Assert.Equal(ProductId, model.Product.ProductId);

            _productServiceMock.Verify(x => x.AddProductAsync(It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public async Task Test_Edit_Get_NoProductFound_ShouldRedirect()
        {
            var result = (await _productController.Edit(NotFoundProductId)) as RedirectToActionResult;

            Assert.NotNull(result);

            _categoryServiceMock.Verify(x => x.GetCategoriesAsync(), Times.Never);
            _supplierServiceMock.Verify(x => x.GetSuppliersAsync(), Times.Never);
        }

        [Fact]
        public async Task Test_Edit_Get_ProductFound_ShouldReturnModelWithFoundProduct()
        {
            var result = (await _productController.Edit(ProductId)) as ViewResult;
            var model = result?.Model as EditProductViewModel;

            Assert.NotNull(model);
            Assert.NotNull(model.Product);
            Assert.Equal(ProductId, model.Product.ProductId);

            _categoryServiceMock.Verify(x => x.GetCategoriesAsync(), Times.Once);
            _supplierServiceMock.Verify(x => x.GetSuppliersAsync(), Times.Once);
        }

        [Fact]
        public async Task Test_Edit_Post_ModelIsValid_ShouldEditProduct()
        {
            var editModel = new EditProductViewModel
            {
                Product = new ProductViewModel { ProductId = ProductId }
            };
            var result = (await _productController.Edit(editModel)) as RedirectToActionResult;

            Assert.NotNull(result);

            _productServiceMock.Verify(x => x.EditProductAsync(It.Is<Product>(y => y.ProductId == ProductId)), Times.Once);
        }

        [Fact]
        public async Task Test_Edit_Post_ModelHasErrors_ShouldNotEditProduct()
        {
            var editModel = new EditProductViewModel
            {
                Product = new ProductViewModel { ProductId = ProductId }
            };

            _productController.ModelState.AddModelError("ProductName", "Some Error");

            var result = (await _productController.Edit(editModel)) as ViewResult;
            var model = result?.Model as EditProductViewModel;

            Assert.NotNull(model);
            Assert.NotNull(model.Product);
            Assert.Equal(ProductId, model.Product.ProductId);

            _productServiceMock.Verify(x => x.EditProductAsync(It.IsAny<Product>()), Times.Never);
        }
    }
}