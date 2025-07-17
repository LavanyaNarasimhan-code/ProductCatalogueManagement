using AutoMapper;
using Moq;
using ProductCatalogue.Application.Product.Queries;
using ProductCatalogue.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogue.Application.Tests.Queries
{
    public class GetProductsByCategoryIdQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnProductsByCategoryId()
        {
            // Arrange
            var _mediator = new Mock<MediatR.IMediator>();
            var _productRepository = new Mock<IProductRepository>();
            var _mapper = new Mock<IMapper>();
            const int categoryId = 1;
            var products = new List<Domain.Entities.Product>
            {
                new Domain.Entities.Product("Product1", 10.0m, 100, categoryId),
                new Domain.Entities.Product("Product2", 20.0m, 200, categoryId)
            };
            _productRepository.Setup(p => p.GetProductsByCategoryIdAsync(categoryId))
                .ReturnsAsync(products);
            _mapper.Setup(m => m.Map<IEnumerable<ProductDto>>(It.IsAny<IEnumerable<Domain.Entities.Product>>()))
                .Returns(new List<ProductDto>
                {
                    new ProductDto { ProductName = "Product1", Price = 10.0m, Stock = 100, CategoryId = categoryId },
                    new ProductDto { ProductName = "Product2", Price = 20.0m, Stock = 200, CategoryId = categoryId }
                });
            var handler = new GetProductsByCategoryIdQueryHandler(_productRepository.Object, _mapper.Object);
            var query = new GetProductsByCategoryIdQuery(categoryId);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}
