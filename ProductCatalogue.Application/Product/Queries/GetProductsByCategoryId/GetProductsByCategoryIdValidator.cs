﻿using FluentValidation;

namespace ProductCatalogue.Application.Product.Queries
{
    public class GetProductsByCategoryIdValidator : AbstractValidator<GetProductsByCategoryIdQuery>
    {
        public GetProductsByCategoryIdValidator()
        {
            RuleFor(x => x.CategoryId).GreaterThan(0)
                .WithMessage("CategoryId must be greater than 0.");
        }
    }
}
