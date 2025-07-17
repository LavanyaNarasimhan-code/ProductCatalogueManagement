using FluentValidation;

namespace ProductCatalogue.Application.Product.Commands
{
    public class AssignProductCategoryCommandValidator : AbstractValidator<AssignProductCategoryCommand>
    {
        public AssignProductCategoryCommandValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0)
                .WithMessage("ProductId must be greater than 0.");
            RuleFor(x => x.CategoryId).GreaterThan(0)
                .WithMessage("CategoryId must be greater than 0.");
        }
    }
}
