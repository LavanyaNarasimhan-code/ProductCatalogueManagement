
using FluentValidation;

namespace ProductCatalogue.Application.Product.Commands
{
    public class UpdateProductPriceCommandValidator : AbstractValidator<UpdateProductPriceCommand>
    {
        public UpdateProductPriceCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("ProductId must be greater than zero.");
            RuleFor(x => x.UpdatedPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than zero.");            
        }
    }
}
