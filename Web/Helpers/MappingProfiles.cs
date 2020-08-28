using Core.Entities;
using Web.DTOs;
using AutoMapper;
namespace Web.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.ProductBrand , o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType , o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
        }
        
    }
}