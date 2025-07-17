using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalogue.Domain.Entities;
using System;
using System.Linq;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("IntegrationTesting");
        builder.ConfigureServices(static services =>
        {            
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<ProductCatalogueDbContext>));

            if (descriptor != null)
                services.Remove(descriptor);
           
            services.AddDbContext<ProductCatalogueDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });
            
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ProductCatalogueDbContext>();
            db.Database.EnsureCreated();
            
            var categories = new[]
            {
                new Category("Electronics") ,
                new Category("Books"),               
            };

            db.Categories.AddRange(categories);

            // 🌱 Seed products
            var products = new[]
            {
                new Product("Smartphone", 599.99m, 50, 1),
                new Product("Laptop", 1299.99m, 30, 1),
                new Product("Harry Potter", 19.99m, 100, 2),
                new Product("The Hobbit", 15.99m, 80, null)
            };

            db.Products.AddRange(products);

            db.SaveChanges();
        });
    }
}
