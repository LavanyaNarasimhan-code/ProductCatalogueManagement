using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using ProductCatalogue.Contracts;

namespace ProductCatalogue.Application.Product.Queries
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(request.ProductId);
            if (product == null)
            {
                throw new ValidationException(new List<ValidationFailure>
                    {
                        new ValidationFailure("ProductId", $"Product with ID {request.ProductId} not found.")
                    });
            }
            return _mapper.Map<ProductDto>(product);
        }
    }
}
