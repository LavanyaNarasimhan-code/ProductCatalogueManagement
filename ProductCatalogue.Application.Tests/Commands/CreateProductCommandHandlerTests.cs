using AutoMapper;
using MediatR;
using Moq;
using ProductCatalogue.Application.Product.Commands;
using ProductCatalogue.Contracts;
using ProductCatalogue.Domain.Entities;
using ProductCatalogue.Domain.Events.ProductEvents;

public class CreateProductCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldPublishEventOnSaveChanges()
    {
        // Arrange
        var mediator = new Mock<IMediator>();
        var dbContext = new Mock<ProductCatalogueDbContext>();
        var productRepository = new Mock<IProductRepository>();

        var mockMapper = new Mock<IMapper>();

        mockMapper.Setup(m => m.Map<Product>(It.IsAny<CreateProductCommand>()))
                  .Returns(new Product("Product A", 100m, 10, null));
        productRepository.Setup(p => p.CreateProductAsync(It.IsAny<Product>()));

        var handler = new CreateProductCommandHandler(productRepository.Object, mediator.Object, mockMapper.Object);

        var command = new CreateProductCommand("Product A", 100, 10, null);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        productRepository.Verify(p => p.CreateProductAsync(It.IsAny<Product>()), Times.Once);
        mediator.Verify(m => m.Publish(It.IsAny<ProductCreatedEvent>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
