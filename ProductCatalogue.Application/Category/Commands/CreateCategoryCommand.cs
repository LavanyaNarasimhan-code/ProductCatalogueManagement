using MediatR;
using ProductCatalogue.Application.Category.Queries;

namespace ProductCatalogue.Application.Category.Commands
{
    public record CreateCategoryCommand(string Name) : IRequest<CategoryDto>;
}
