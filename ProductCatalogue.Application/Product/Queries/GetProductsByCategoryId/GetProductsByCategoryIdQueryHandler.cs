using AutoMapper;
using MediatR;
using ProductCatalogue.Contracts;

namespace ProductCatalogue.Application.Product.Queries
{
    public class GetProductsByCategoryIdQueryHandler : IRequestHandler<GetProductsByCategoryIdQuery, List<ProductDto>>
    {
        private readonly IProductRepository _productRepository;
        private IMapper _mapper;
        public GetProductsByCategoryIdQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<List<ProductDto>> Handle(GetProductsByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetProductsByCategoryIdAsync(request.CategoryId);
            
            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
