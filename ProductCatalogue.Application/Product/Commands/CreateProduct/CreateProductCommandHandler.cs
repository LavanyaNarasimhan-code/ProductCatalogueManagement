using AutoMapper;
using MediatR;
using ProductCatalogue.Application.Product.Queries;
using ProductCatalogue.Contracts;

namespace ProductCatalogue.Application.Product.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductRepository productRepository, IMediator mediator, IMapper mapper)
        {
            _productRepository = productRepository;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {   
            var product = new Domain.Entities.Product(
                request.ProductName,                
                request.Price,
                request.Stock,
                request.CategoryId
            );
            await _productRepository.CreateProductAsync(product);
            
            await _mediator.Publish(new Domain.Events.ProductEvents.ProductCreatedEvent(product), cancellationToken);

            return _mapper.Map<ProductDto>(product);
        }
    }
}
