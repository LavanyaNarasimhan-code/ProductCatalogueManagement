using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCatalogue.Domain.Entities;

namespace ProductCatalogue.Domain.Events.ProductEvents
{
    public class ProductStockUpdatedEvent : DomainEvent
    {
        public int ProductId { get; }
        public decimal UpdateStock { get; }
        public ProductStockUpdatedEvent(int productId, int updatedStock) 
        {
            ProductId = productId;
            UpdateStock = updatedStock;
        }
    }
}
