using AutoMapper;
using ProductCatalogue.Application.Category.Queries;

namespace ProductCatalogue.Application.MappingProfile
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Domain.Entities.Category, CategoryDto>().ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Id));
        }
    }    
}
