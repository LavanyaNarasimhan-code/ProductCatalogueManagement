using MediatR;
using Moq;
using ProductCatalogue.Application.Product.Commands;
using ProductCatalogue.Contracts;
using ProductCatalogue.Domain.Events.ProductEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogue.Application.Tests.Commands
{
    public class AssignProductCategoryCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldAssignCategoryToProduct()
        {
            // Arrange
            var _mediator = new Mock<IMediator>();
            var _productRepository = new Mock<IProductRepository>();
            const int productId = 1;
            const int categoryId = 2;
            var handler = new AssignProductCategoryCommandHandler(_productRepository.Object, _mediator.Object);
            var command = new AssignProductCategoryCommand(productId, categoryId);
            _productRepository.Setup(p => p.AssignCategoryForProductAsync(It.IsIn(productId), It.IsIn(categoryId)))
                .ReturnsAsync(true);
            // Act
            await handler.Handle(command, CancellationToken.None);
            // Assert
            _mediator.Verify(m => m.Publish(
                It.Is<CategoryAssignedForProductEvent>(e => e.ProductId == productId && e.CategoryId == categoryId),
                It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
