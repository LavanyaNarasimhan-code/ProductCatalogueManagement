namespace ProductCatalogue.Contracts
{
    public interface IProductRepository
    {
        Task CreateProductAsync(Domain.Entities.Product product);        
        Task<bool> UpdateProductPriceAsync(int productId, decimal price);
        Task<bool> UpdateProductStockAsync(int productId, int stock);
        Task<bool> AssignCategoryForProductAsync(int productId, int categoryId);
        Task<Domain.Entities.Product?> GetProductByIdAsync(int id);
        Task<IEnumerable<Domain.Entities.Product>> GetProductsByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Domain.Entities.Product>> GetAllProductsAsync();
    }
}
