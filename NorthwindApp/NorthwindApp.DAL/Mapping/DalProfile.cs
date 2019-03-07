using AutoMapper;
using NorthwindApp.DAL.Entities;
using NorthwindApp.Models;

namespace NorthwindApp.DAL.Mapping
{
    public class DalProfile : Profile
    {
        public DalProfile()
        {
            CreateMap<ProductDto, Product>();
            CreateMap<CategoryDto, Category>();
            CreateMap<SupplierDto, Supplier>();
        }
    }
}
