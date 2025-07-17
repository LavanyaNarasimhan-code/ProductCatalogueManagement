using Microsoft.EntityFrameworkCore;
using ProductCatalogue.Domain.Entities;

public class ProductCatalogueDbContext : DbContext
{
    public ProductCatalogueDbContext(DbContextOptions<ProductCatalogueDbContext> options) : base(options) 
    {
        
    }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductCatalogueDbContext).Assembly);
        base.OnModelCreating(modelBuilder);        
    }

    
}