using AutoMapper;
using NorthwindApp.DAL.Entities;
using NorthwindApp.Models;

namespace NorthwindApp.DAL.Mapping
{
    public class DalProfile : Profile
    {
        public DalProfile()
        {
            CreateMap<CategoryDto, BaseCategory>();
            CreateMap<SupplierDto, BaseSupplier>();

            CreateMap<ProductDto, Product>();
            CreateMap<CategoryDto, Category>();
            CreateMap<SupplierDto, Supplier>();

            CreateMap<Product, ProductDto>()
                .ForMember(x => x.CategoryId, x => x.MapFrom(y => y.Category.CategoryId))
                .ForMember(x => x.SupplierId, x => x.MapFrom(y => y.Supplier.SupplierId))
                .ForMember(x => x.Category, x => x.Ignore())
                .ForMember(x => x.Supplier, x => x.Ignore());
        }
    }
}
