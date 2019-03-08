using AutoMapper;
using NorthwindApp.Models;
using NorthwindApp.UI.Models;

namespace NorthwindApp.DAL.Mapping
{
    public class ViewModelProfile : Profile
    {
        public ViewModelProfile()
        {
            CreateMap<BaseCategory, BaseCategoryViewModel>();
            CreateMap<BaseSupplier, BaseSupplierViewModel>();

            CreateMap<Product, ProductViewModel>();
            CreateMap<Category, CategoryViewModel>();
        }
    }
}
