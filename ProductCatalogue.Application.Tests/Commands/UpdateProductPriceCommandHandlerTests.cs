using MediatR;
using Microsoft.IdentityModel.Tokens;
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
    public class UpdateProductPriceCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldPublishEventOnPriceChange()
        {
            //Arrange
            var _mediator = new Mock<IMediator>();
            var _productRepository = new Mock<IProductRepository>();


            const int productId = 1;
            const decimal updatedPrice = 10.5m;

            var handler = new UpdateProductPriceCommandHandler(_productRepository.Object, _mediator.Object);
            var command = new UpdateProductPriceCommand(productId, updatedPrice);

            _productRepository.Setup(p => p.UpdateProductPriceAsync(It.IsIn(productId), It.IsIn(updatedPrice)))
                .ReturnsAsync(true);


            //Act
            await handler.Handle(command, CancellationToken.None);

            _mediator.Verify(m => m.Publish(
        It.Is<ProductPriceUpdatedEvent>(e => e.ProductId == productId && e.UpdatedPrice == updatedPrice),
        It.IsAny<CancellationToken>()),
        Times.Once);
        }
    }
}
