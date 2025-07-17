using Microsoft.EntityFrameworkCore;
using ProductCatalogue.Contracts;
using ProductCatalogue.Domain.Entities;

namespace ProductCatalogue.Persistence.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductCatalogueDbContext _context;
        private readonly ICategoryRepository _categoryRepository;
        public ProductRepository(ProductCatalogueDbContext context, ICategoryRepository categoryRepository)
        {
            _context = context;
            _categoryRepository = categoryRepository;
        }
        public async Task CreateProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _context.Products.Where(x => x.CategoryId == categoryId).ToListAsync();
        }

        public async Task<bool> UpdateProductPriceAsync(int productId, decimal price)
        {
            var product = await GetProductByIdAsync(productId);
            if (product == null)
            {
                return false;
            }
            if (product.Price == price)
            {
                return true;
            }            
            product.Price = price;
            product.UpdatedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateProductStockAsync(int productId, int stock)
        {
            var product = await GetProductByIdAsync(productId);
            if (product == null)
            {
                return false;
            }
            if(product.Stock == stock)
            {
                return true;
            }
            product.Stock = stock;
            product.UpdatedDate = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AssignCategoryForProductAsync(int productId, int categoryId)
        {
            var product = await GetProductByIdAsync(productId);
            if (product == null)
            {
                return false;
            }
            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            if (category == null)
            {
                return false;
            }
            if (product.CategoryId == categoryId)
            {
                return true;
            }            
            product.CategoryId = categoryId;
            product.Category = category;
            product.UpdatedDate = DateTime.UtcNow;            
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
