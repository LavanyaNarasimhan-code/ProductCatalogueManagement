using AutoMapper;
using MediatR;
using ProductCatalogue.Application.Category.Queries;
using ProductCatalogue.Contracts;
using ProductCatalogue.Domain.Events.CategoryEvents;


namespace ProductCatalogue.Application.Category.Commands
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMediator mediator, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Domain.Entities.Category(request.Name);
            await _categoryRepository.CreateCategoryAsync(category);

            await _mediator.Publish(new CategoryCreatedEvent(category));

            return _mapper.Map<CategoryDto>(category);

        }
    }
}
