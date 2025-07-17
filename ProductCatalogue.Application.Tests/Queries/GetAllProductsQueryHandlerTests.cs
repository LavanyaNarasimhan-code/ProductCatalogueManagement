using AutoMapper;
using MediatR;
using ProductCatalogue.Application.Product.Queries;
using ProductCatalogue.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogue.Application.Tests.Queries
{
    public class GetAllProductsQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnAllProducts()
        {
            // Arrange
            var _mediator = new Mock<IMediator>();
            var _productRepository = new Mock<IProductRepository>();
            var _mapper = new Mock<IMapper>();
            var products = new List<Domain.Entities.Product>
            {
                new Domain.Entities.Product("Product1", 10.0m, 100, 1),
                new Domain.Entities.Product("Product2", 20.0m, 200, 2)
            };
            _productRepository.Setup(p => p.GetAllProductsAsync())
                .ReturnsAsync(products);
            _mapper.Setup(m => m.Map<IEnumerable<ProductDto>>(It.IsAny<IEnumerable<Domain.Entities.Product>>()))
                .Returns(new List<ProductDto>
                {
                    new ProductDto { ProductName = "Product1", Price = 10.0m, Stock = 100, CategoryId = 1 },
                    new ProductDto { ProductName = "Product2", Price = 20.0m, Stock = 200, CategoryId = 2 }
                });
            var handler = new GetAllProductsQueryHandler(_productRepository.Object, _mapper.Object);

            // Act
            var result = await handler.Handle(new GetAllProductsQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}
