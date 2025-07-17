using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductCatalogue.Api.Controllers;
using ProductCatalogue.Application.Product.Commands;
using ProductCatalogue.Application.Product.Queries;
using ProductCatalogue.Domain.Entities;
using System.Net;

public class ProductControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly ProductsController _controller;

    public ProductControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new ProductsController(_mediatorMock.Object);
    }

    [Fact]
    public async Task Create_ShouldReturnCreatedResult_WhenCommandIsValid()
    {
        // Arrange
        var command = new CreateProductCommand("Test Product", 100, 10, null);
        var generatedId = 1;

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ProductDto { ProductId = generatedId, ProductName = "Test Product", CategoryId = null, Price = 100, Stock = 10});

        // Act
        var result = await _controller.Create(command);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
        var returnDto = Assert.IsType<ProductDto>(okResult.Value);
        Assert.Equal(generatedId, returnDto.ProductId);
    }

    [Fact]
    public async Task GetById_ShouldReturnOk_WhenProductExists()
    {
        // Arrange
        const int productId = 1;
        var dto = new ProductDto { ProductId = productId, ProductName = "Product A", Price = 99, Stock = 10 };

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetProductByIdQuery>(q => q.ProductId == productId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(dto);

        // Act
        var result = await _controller.GetById(productId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnDto = Assert.IsType<ProductDto>(okResult.Value);
        Assert.Equal(productId, returnDto.ProductId);
    }    

    [Fact]
    public async Task GetAll_ShouldReturnOkWithList()
    {
        // Arrange
        var products = new List<ProductDto>
        {
            new ProductDto { ProductId = 1, ProductName = "Product 1", Price = 100, Stock = 10 },
            new ProductDto { ProductId = 2, ProductName = "Product 2", Price = 200, Stock = 20 }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetAllProductsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(products);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnList = Assert.IsAssignableFrom<IEnumerable<ProductDto>>(okResult.Value);
        Assert.Equal(2, returnList.Count());
    }


    [Fact]
    public async Task UpdatePrice_ShouldReturnOk_WhenPriceIsUpdated()
    {
        // Arrange
        var command = new UpdateProductPriceCommand(1, 105);

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask); 

        // Act
        var result = await _controller.UpdatePrice(command);

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(200, okResult.StatusCode); 

        _mediatorMock.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateStock_ShouldReturnOk_WhenStockIsUpdated()
    {
        // Arrange
        var command = new UpdateProductStockCommand(1, 11);

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask); 

        // Act
        var result = await _controller.UpdateStock(command);

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(200, okResult.StatusCode); 

        _mediatorMock.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateCategory_ShouldReturnOk_WhenCategoryIsUpdated()
    {
        // Arrange
        var command = new AssignProductCategoryCommand(1, 10);

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask); 

        // Act
        var result = await _controller.UpdateCategory(command);

        // Assert
        Assert.IsType<OkResult>(result);

        _mediatorMock.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetByCategoryId_ShouldReturnOkWithProductList()
    {
        // Arrange
        const int categoryId = 20;

        var products = new List<ProductDto>
    {
        new ProductDto { ProductId = 2, ProductName = "Product 2", Price = 200, Stock = 20, CategoryId = 20 },
        new ProductDto { ProductId = 3, ProductName = "Product 3", Price = 250, Stock = 30, CategoryId = 20 }
    };

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetProductsByCategoryIdQuery>(q => q.CategoryId == categoryId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(products);

        // Act
        var result = await _controller.GetByCategoryId(categoryId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnList = Assert.IsAssignableFrom<IEnumerable<ProductDto>>(okResult.Value);

        var productList = returnList.ToList();
        Assert.Equal(2, productList.Count);

        Assert.Collection(productList,
            p =>
            {
                Assert.Equal(2, p.ProductId);
                Assert.Equal("Product 2", p.ProductName);
                Assert.Equal(200, p.Price);
                Assert.Equal(20, p.Stock);
                Assert.Equal(20, p.CategoryId);
            },
            p =>
            {
                Assert.Equal(3, p.ProductId);
                Assert.Equal("Product 3", p.ProductName);
                Assert.Equal(250, p.Price);
                Assert.Equal(30, p.Stock);
                Assert.Equal(20, p.CategoryId);
            });

        _mediatorMock.Verify(m => m.Send(It.Is<GetProductsByCategoryIdQuery>(q => q.CategoryId == categoryId), It.IsAny<CancellationToken>()), Times.Once);
    }
}
