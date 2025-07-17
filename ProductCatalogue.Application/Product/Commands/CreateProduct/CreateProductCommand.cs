using MediatR;
using ProductCatalogue.Application.Product.Queries;

namespace ProductCatalogue.Application.Product.Commands
{
    public record CreateProductCommand(string ProductName, decimal Price, int Stock, int? CategoryId) : IRequest<ProductDto>;
    
}
