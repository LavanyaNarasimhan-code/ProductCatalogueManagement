using FluentValidation;

namespace ProductCatalogue.Application.Product.Queries
{
    public class GetProductByIdValidator : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0)
                .WithMessage("ProductId must be greater than 0.");
        }
    }
}
