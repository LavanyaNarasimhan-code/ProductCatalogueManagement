using FluentAssertions;
using FluentValidation.Results;
using ProductCatalogue.Application.Category.Queries;
using ProductCatalogue.Application.Product.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace ProductCatalogue.Integration.Tests;
public class CategoriesControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public CategoriesControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetCategories_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/categories/GetAll");
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetCategoryById_Returns400_WhenCategoryIdIsNotValid()
    {
        var response = await _client.GetAsync("/api/categories/GetById?categoryId=0");
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await response.Content.ReadFromJsonAsync<List<ValidationFailure>>();
        errors.Should().NotBeNull();
        errors.Should().ContainSingle(e => e.PropertyName == "CategoryId" && e.ErrorMessage == "CategoryId must be greater than 0.");
    }
    [Fact]
    public async Task GetCategoryById_Returns200_WithValidCategoryId()
    {
        // Arrange
        const int categoryId = 1;

        // Act
        var response = await _client.GetAsync($"/api/categories/GetById?categoryId=1");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var category = await response.Content.ReadFromJsonAsync<CategoryDto>();
        category.Should().NotBeNull();        
        
    }
}