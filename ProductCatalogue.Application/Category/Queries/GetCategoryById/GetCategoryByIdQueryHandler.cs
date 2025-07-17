using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using ProductCatalogue.Contracts;

namespace ProductCatalogue.Application.Category.Queries
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
    {
        ICategoryRepository _categoryRepository;
        IMapper _mapper;
        public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(request.CategoryId);
            if (category == null)
            {
                throw new ValidationException(new List<ValidationFailure>
                    {
                        new ValidationFailure("CategoryId", $"Category with ID {request.CategoryId} not found.")
                    });
            }
            return _mapper.Map<CategoryDto>(category);
        }
    }
}
