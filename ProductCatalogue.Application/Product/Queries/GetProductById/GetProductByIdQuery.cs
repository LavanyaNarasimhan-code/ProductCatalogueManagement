using MediatR;

namespace ProductCatalogue.Application.Product.Queries
{
    public record GetProductByIdQuery(int ProductId) : IRequest<ProductDto>;
}
