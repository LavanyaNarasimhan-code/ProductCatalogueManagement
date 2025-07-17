using MediatR;

namespace ProductCatalogue.Application.Category.Queries
{
    public record GetCategoryByIdQuery(int CategoryId) : IRequest<CategoryDto>;
    
}
