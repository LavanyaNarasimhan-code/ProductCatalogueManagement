
using MediatR;

namespace ProductCatalogue.Application.Product.Queries
{
    public record GetAllProductsQuery() : IRequest<List<ProductDto>>;
}
