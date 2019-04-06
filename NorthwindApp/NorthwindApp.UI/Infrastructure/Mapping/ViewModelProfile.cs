using AutoMapper;
using NorthwindApp.Models;
using NorthwindApp.UI.Infrastructure.Configuration;
using NorthwindApp.UI.Models;

namespace NorthwindApp.UI.Infrastructure.Mapping
{
    public class ViewModelProfile : Profile
    {
        public ViewModelProfile()
        {
            CreateMap<BaseCategory, BaseCategoryViewModel>();
            CreateMap<BaseSupplier, BaseSupplierViewModel>();

            CreateMap<Product, ProductViewModel>();
            CreateMap<Category, CategoryViewModel>();

            CreateMap<Breadcrumb, BreadcrumbViewModel>();
        }
    }
}
