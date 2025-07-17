using FluentValidation.TestHelper;
using ProductCatalogue.Application.Product.Commands;
using Xunit;

public class CreateProductCommandValidatorTests
{
    private readonly CreateProductCommandValidator _validator;

    public CreateProductCommandValidatorTests()
    {
        _validator = new CreateProductCommandValidator();
    }

    [Fact]
    public void Should_PassValidation_When_CommandIsValid()
    {
        // Arrange
        var command = new CreateProductCommand("Valid Product", 100m, 10, 1);

        // Act & Assert
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Fail_When_ProductNameIsEmpty()
    {
        var command = new CreateProductCommand(string.Empty, 100m, 10, 1);

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ProductName);
    }

    [Fact]
    public void Should_Fail_When_ProductNameIsTooLong()
    {
        var longName = new string('A', 101);
        var command = new CreateProductCommand(longName, 100m, 10, 1);

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ProductName);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void Should_Fail_When_PriceIsZeroOrNegative(decimal price)
    {
        var command = new CreateProductCommand("Product", price, 10, 1);

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Price);
    }

    [Fact]
    public void Should_Fail_When_StockIsNegative()
    {
        var command = new CreateProductCommand("Product", 100m, -1, 1);

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Stock);
    }

    [Fact]
    public void Should_Fail_When_CategoryIdIsLessThanOrEqualToZero()
    {
        var command = new CreateProductCommand("Product", 100m, 10, 0);

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.CategoryId);
    }

    [Fact]
    public void Should_Pass_When_CategoryIdIsNull()
    {
        var command = new CreateProductCommand("Product", 100m, 10, null);

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.CategoryId);
    }
}