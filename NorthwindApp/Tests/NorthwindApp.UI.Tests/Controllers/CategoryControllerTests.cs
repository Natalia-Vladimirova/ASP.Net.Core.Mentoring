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

namespace NorthwindApp.UI.Tests.Controllers
{
    public class CategoryControllerTests
    {
        private readonly CategoryController _categoryController;

        private readonly Mock<ICategoryService> _categoryServiceMock;
        private readonly Mock<IMapper> _mapperMock;

        private const int CategoryId = 55;
        private const string CategoryName = "Fantastic Chocolate";

        public CategoryControllerTests()
        {
            _categoryServiceMock = new Mock<ICategoryService>();
            _categoryServiceMock.Setup(x => x.GetCategoriesAsync())
                .ReturnsAsync(new[] { new Category { CategoryId = CategoryId, CategoryName = CategoryName } });

            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(x => x.Map<CategoryViewModel>(It.IsAny<Category>()))
                .Returns((Category category) => new CategoryViewModel
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName
                });

            _categoryController = new CategoryController(_categoryServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Test_Index_ShouldReturnCategories()
        {
            var result = (await _categoryController.Index()) as ViewResult;
            var model = result?.Model as IEnumerable<CategoryViewModel>;

            Assert.NotNull(model);
            var categories = model.ToArray();

            Assert.Single(categories);
            Assert.Equal(CategoryId, categories[0].CategoryId);
            Assert.Equal(CategoryName, categories[0].CategoryName);

            _categoryServiceMock.Verify(x => x.GetCategoriesAsync(), Times.Once);
            _mapperMock.Verify(x => x.Map<CategoryViewModel>(It.Is<Category>(y => y.CategoryId == CategoryId)), Times.Once);
        }
    }
}
