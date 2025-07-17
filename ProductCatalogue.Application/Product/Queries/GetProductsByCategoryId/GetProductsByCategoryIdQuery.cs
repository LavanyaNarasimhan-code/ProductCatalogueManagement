using MediatR;

namespace ProductCatalogue.Application.Product.Queries
{
    public record GetProductsByCategoryIdQuery(int CategoryId) : IRequest<List<ProductDto>>;    
}
