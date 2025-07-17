
using FluentValidation;

namespace ProductCatalogue.Application.Product.Commands
{
    public class UpdateProductStockCommandValidator : AbstractValidator<UpdateProductStockCommand>
    {
        public UpdateProductStockCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("ProductId must be greater than zero.");
            RuleFor(x => x.UpdatedStock)
                .GreaterThanOrEqualTo(0).WithMessage("Stock must be greater than zero.");
        }
    }
}
