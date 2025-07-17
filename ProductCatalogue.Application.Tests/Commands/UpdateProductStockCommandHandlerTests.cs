using MediatR;
using Moq;
using ProductCatalogue.Application.Product.Commands;
using ProductCatalogue.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogue.Application.Tests.Commands
{
    public class UpdateProductStockCommandHandlerTests
    {
       
        [Fact]
        public async Task Handle_ShouldPublishEventOnStockUpdate()
        {
            // Arrange
            var _mediator = new Mock<IMediator>();
            var _productRepository = new Mock<IProductRepository>();

            const int productId = 1;
            const int updatedStock = 100;

            _mediator = new Mock<IMediator>();
            _productRepository = new Mock<IProductRepository>();
            var handler = new UpdateProductStockCommandHandler(_productRepository.Object, _mediator.Object);
            var command = new UpdateProductStockCommand(productId, updatedStock);
            _productRepository.Setup(p => p.UpdateProductStockAsync(It.IsIn(productId), It.IsIn(updatedStock)))
                .ReturnsAsync(true);
            // Act
            await handler.Handle(command, CancellationToken.None);
            // Assert
            _mediator.Verify(m => m.Publish(
                It.Is<Domain.Events.ProductEvents.ProductStockUpdatedEvent>(e => e.ProductId == productId && e.UpdateStock == updatedStock),
                It.IsAny<CancellationToken>()),
                Times.Once);
        }

    }
}
