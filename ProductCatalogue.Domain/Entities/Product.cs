using ProductCatalogue.Domain.Common;
using ProductCatalogue.Domain.Events;
using ProductCatalogue.Domain.Events.ProductEvents;

namespace ProductCatalogue.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        private Product() { }

        public Product(string productName, decimal price, int stock, int? categoryId)
        {
            ProductName = productName;
            this.Price = price;
            this.Stock = stock;
            CategoryId = categoryId;
            CreatedDate = DateTime.UtcNow;
            UpdatedDate = DateTime.UtcNow;
        }
    }
}
