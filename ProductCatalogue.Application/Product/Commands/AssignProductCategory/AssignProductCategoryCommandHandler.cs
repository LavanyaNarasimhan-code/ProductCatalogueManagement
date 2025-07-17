using FluentValidation;
using FluentValidation.Results;
using MediatR;
using ProductCatalogue.Contracts;
using ProductCatalogue.Domain.Events.ProductEvents;


namespace ProductCatalogue.Application.Product.Commands
{
    public class AssignProductCategoryCommandHandler : IRequestHandler<AssignProductCategoryCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMediator _mediator;

        public AssignProductCategoryCommandHandler(IProductRepository productRepository, IMediator mediator)
        {
            _productRepository = productRepository;
            _mediator = mediator;
        }

        public async Task Handle(AssignProductCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = await _productRepository.AssignCategoryForProductAsync(request.ProductId, request.CategoryId);

            if(!result)
            {
                throw new ValidationException(new List<ValidationFailure>
                    {
                        new ValidationFailure("", "Product or Category not found")
                    });
            }
            
            await _mediator.Publish(new CategoryAssignedForProductEvent(request.ProductId, request.CategoryId), cancellationToken);
        }
    }
}
