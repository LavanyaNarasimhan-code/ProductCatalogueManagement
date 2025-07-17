using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogue.Application.Category.Commands;
using ProductCatalogue.Application.Category.Queries;

namespace ProductCatalogue.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryCommand command)
        {
            var category = await _mediator.Send(command);
            return Ok(category);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int categoryId)
        {
            var category = await _mediator.Send(new GetCategoryByIdQuery(categoryId));
            return Ok(category);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _mediator.Send(new GetAllCategoriesQuery());
            return Ok(categories);
        }
    }
}
