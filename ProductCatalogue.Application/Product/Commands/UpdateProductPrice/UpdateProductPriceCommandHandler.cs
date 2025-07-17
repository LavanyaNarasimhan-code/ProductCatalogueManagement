using FluentValidation;
using FluentValidation.Results;
using MediatR;
using ProductCatalogue.Contracts;

namespace ProductCatalogue.Application.Product.Commands
{
    public class UpdateProductPriceCommandHandler : IRequestHandler<UpdateProductPriceCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMediator _mediator;

        public UpdateProductPriceCommandHandler(IProductRepository productRepository, IMediator mediator)
        {
            _productRepository = productRepository;            
            _mediator = mediator;
        }

        public async Task Handle(UpdateProductPriceCommand request, CancellationToken cancellationToken)
        {            
            var result = await _productRepository.UpdateProductPriceAsync(request.ProductId, request.UpdatedPrice);
            if (!result)
            {
                throw new ValidationException(new List<ValidationFailure>
                    {
                        new ValidationFailure("ProductId", "Product not found")
                    });
            }
            
            await _mediator.Publish(new Domain.Events.ProductEvents.ProductPriceUpdatedEvent(request.ProductId, request.UpdatedPrice), cancellationToken);
        }
    }
}
