using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductCatalogue.Api.Controllers;
using ProductCatalogue.Application.Category.Commands;
using ProductCatalogue.Application.Category.Queries;


namespace ProductCatalogue.Api.Tests
{
    public class CategoriesControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly CategoriesController _Controller;

        public CategoriesControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _Controller = new CategoriesController(_mediatorMock.Object);
        }

        [Fact]
        public async Task Create_ShouldReturnCreatedResult_WhenCommandIsValid()
        {
            //Arrange
            var command = new CreateCategoryCommand("Test Category");
            const int categoryId = 10;

            _mediatorMock.Setup(s => s.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new CategoryDto { CategoryId = categoryId, CategoryName = "Test Category", });

            //Act
            var result = await _Controller.Create(command);

            //Assert
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
            var returnDto = Assert.IsType<CategoryDto>(okResult.Value);
            Assert.Equal(categoryId, returnDto.CategoryId);
        }

        [Fact]
        public async Task GetById_ShouldReturnOk_WhenCategoryExist()
        {
            //Arrange
            var query = new GetCategoryByIdQuery(10);
            const int categoryId = 10;


            _mediatorMock.Setup(s => s.Send(query, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new CategoryDto { CategoryId = categoryId, CategoryName = "Test Category" });

            //Act
            var result = await _Controller.GetById(categoryId);
            
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnDto = Assert.IsType<CategoryDto>(okResult.Value);
            Assert.Equal(categoryId, returnDto.CategoryId);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkWithList()
        {
            //Arrange
            var query = new GetAllCategoriesQuery();

            var categoryDto = new List<CategoryDto>
            {
               new CategoryDto { CategoryId = 10, CategoryName = "Test Category 1"},
               new CategoryDto {CategoryId = 20, CategoryName = "Test Category 2"}
            };

            _mediatorMock.Setup(s => s.Send(query, It.IsAny<CancellationToken>()))
                .ReturnsAsync(categoryDto);

            //Act
            var result = await _Controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnList = Assert.IsAssignableFrom<IEnumerable<CategoryDto>>(okResult.Value);
            Assert.Equal(2, returnList.Count());
        }
    }
}

