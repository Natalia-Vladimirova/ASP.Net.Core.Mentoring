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
        }
    }
}
