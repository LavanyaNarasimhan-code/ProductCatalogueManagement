using System;
using System.Collections.Generic;
using Moq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProductCatalogue.Contracts;
using AutoMapper;
using ProductCatalogue.Application.Product.Queries;

namespace ProductCatalogue.Application.Tests.Queries
{
    public class GetProductByIdQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            var _mediator = new Mock<IMediator>();
            var _productRepository = new Mock<IProductRepository>();
            var _mapper = new Mock<IMapper>();
            const int productId = 1;
            var product = new Domain.Entities.Product("Test Product", 100.0m, 50, 1);
            _productRepository.Setup(p => p.GetProductByIdAsync(productId))
                .ReturnsAsync(product);
            var productDto = new ProductDto { ProductName = "Test Product", Price = 100.0m, Stock = 50, CategoryId = 1 };
            _mapper.Setup(m => m.Map<ProductDto>(It.IsAny<Domain.Entities.Product>()))
                .Returns(productDto);
            var handler = new GetProductByIdQueryHandler(_productRepository.Object, _mapper.Object);
            var query = new GetProductByIdQuery(productId);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productDto.ProductName, result.ProductName);
        }
    }
}
