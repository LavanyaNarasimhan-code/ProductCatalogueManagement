using AutoMapper;
using ProductCatalogue.Domain.Entities;
using ProductCatalogue.Application.Product.Queries;

namespace ProductCatalogue.Application.MappingProfile
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Domain.Entities.Product, ProductDto>().ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
