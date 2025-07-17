using ProductCatalogue.Domain.Entities;

namespace ProductCatalogue.Contracts
{
    public interface ICategoryRepository
    {
        Task CreateCategoryAsync(Category category);

        Task<Category?> GetCategoryByIdAsync(int categoryId);

        Task<IEnumerable<Category>> GetAllCategoriesAsync();
    }
}
