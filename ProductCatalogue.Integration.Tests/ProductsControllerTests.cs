using FluentAssertions;
using FluentValidation.Results;
using ProductCatalogue.Application.Product.Queries;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace ProductCatalogue.Integration.Tests;
public class ProductsControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ProductsControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetProducts_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/products/GetAll");
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetProductById_Returns400_WhenProductIdIsNotValid()
    {
        var response = await _client.GetAsync("/api/products/GetById?productId=0");

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await response.Content.ReadFromJsonAsync<List<ValidationFailure>>();
        errors.Should().NotBeNull();
        errors.Should().ContainSingle(e => e.PropertyName == "ProductId" && e.ErrorMessage == "ProductId must be greater than 0.");
    }

    [Fact]
    public async Task GetByCategoryId_Returns200_WithValidCategoryId()
    {
        // Arrange
        const int categoryId = 1;

        // Act
        var response = await _client.GetAsync($"/api/products/GetByCategoryId?categoryId={categoryId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var products = await response.Content.ReadFromJsonAsync<List<ProductDto>>();
        products.Should().NotBeNull(); // optional: further assertions on content
    }

    [Fact]
    public async Task UpdateCategory_Returns400_WhenProductIdOrCategoryIdIsInvalid()
    {
        // Arrange
        var invalidCommand = new
        {
            ProductId = 10,
            CategoryId = 10
        };
        
        // Act
        var response = await _client.PutAsJsonAsync("/api/products/UpdateCategory", invalidCommand);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var errors = await response.Content.ReadFromJsonAsync<List<ValidationFailure>>();
        errors.Should().NotBeNull();
        errors.Should().Contain(e => e.ErrorMessage == "Product or Category not found");        
    }
    
    [Fact]
    public async Task UpdateCategory_Returns200_WhenInputIsValid()
    {
        // Arrange
        var validCommand = new
        {
            ProductId = 1,
            CategoryId = 2
        };

        // Act
        var response = await _client.PutAsJsonAsync("/api/Products/UpdateStock", validCommand);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task CreateProduct_Returns400_WhenCommandIsInvalid()
    {
        // Arrange
        var invalidCommand = new
        {
            ProductName = "",      // Invalid: empty
            Price = 0,             // Invalid: not > 0
            Stock = -1,            // Invalid: negative
            CategoryId = 0         // Invalid: must be > 0 if provided
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/products", invalidCommand);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var errors = await response.Content.ReadFromJsonAsync<List<ValidationFailure>>();
        errors.Should().NotBeNull();

        errors.Should().Contain(e => e.PropertyName == "ProductName" && e.ErrorMessage.Contains("cannot be empty"));
        errors.Should().Contain(e => e.PropertyName == "Price" && e.ErrorMessage == "Price must be greater than zero.");
        errors.Should().Contain(e => e.PropertyName == "Stock" && e.ErrorMessage == "Stock cannot be negative.");
        errors.Should().Contain(e => e.PropertyName == "CategoryId" && e.ErrorMessage == "Category ID must be greater than zero if provided.");
    }

    [Fact]
    public async Task CreateProduct_Returns200_WhenCommandIsValid()
    {
        // Arrange
        var validCommand = new
        {
            ProductName = "Test Product",
            Price = 99.99m,
            Stock = 10,
            CategoryId = 1
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/products", validCommand);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<ProductDto>();
        result.Should().NotBeNull();
        result.ProductName.Should().Be("Test Product");
    }
}
