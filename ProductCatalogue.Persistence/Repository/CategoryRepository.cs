using Microsoft.EntityFrameworkCore;
using ProductCatalogue.Contracts;
using ProductCatalogue.Domain.Entities;

namespace ProductCatalogue.Persistence.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ProductCatalogueDbContext _context;

        public CategoryRepository(ProductCatalogueDbContext context)
        {
            _context = context;
        }

        public async Task CreateCategoryAsync(Category category)
        {            
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int categoryId)
        {
            return await _context.Categories.FindAsync(categoryId);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
