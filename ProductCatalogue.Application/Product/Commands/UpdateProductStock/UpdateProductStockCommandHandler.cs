using FluentValidation;
using FluentValidation.Results;
using MediatR;
using ProductCatalogue.Contracts;

namespace ProductCatalogue.Application.Product.Commands
{
    public class UpdateProductStockCommandHandler : IRequestHandler<UpdateProductStockCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMediator _mediator;

        public UpdateProductStockCommandHandler(IProductRepository productRepository, IMediator mediator)
        {
            _productRepository = productRepository;
            _mediator = mediator;            
        }

        public async Task Handle(UpdateProductStockCommand request, CancellationToken cancellationToken)
        {
            var result = await _productRepository.UpdateProductStockAsync(request.ProductId, request.UpdatedStock);
            if (!result)
            {
                throw new ValidationException(new List<ValidationFailure>
                    {
                        new ValidationFailure("ProductId", "Product not found")
                    });
            }
            
            await _mediator.Publish(new Domain.Events.ProductEvents.ProductStockUpdatedEvent(request.ProductId, request.UpdatedStock), cancellationToken);
        }
    }
}
