using MediatR;

namespace ProductCatalogue.Application.Category.Queries
{
    public record GetAllCategoriesQuery() : IRequest<List<CategoryDto>>;
}
