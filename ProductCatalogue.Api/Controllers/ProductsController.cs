using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogue.Application.Product.Commands;
using ProductCatalogue.Application.Product.Queries;

namespace ProductCatalogue.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand command)
        {
            var product = await _mediator.Send(command);
            return Ok(product);
        }

        [HttpPut("UpdateStock")]
        public async Task<IActionResult> UpdateStock(UpdateProductStockCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut("UpdatePrice")]
        public async Task<IActionResult> UpdatePrice(UpdateProductPriceCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(AssignProductCategoryCommand command) 
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int productId)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(productId));
            return Ok(product);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _mediator.Send(new GetAllProductsQuery());
            return Ok(products);
        }

        [HttpGet("GetByCategoryId")]
        public async Task<IActionResult> GetByCategoryId(int categoryId)
        {
            var products = await _mediator.Send(new GetProductsByCategoryIdQuery(categoryId));
            return Ok(products);
        }
    }
}
