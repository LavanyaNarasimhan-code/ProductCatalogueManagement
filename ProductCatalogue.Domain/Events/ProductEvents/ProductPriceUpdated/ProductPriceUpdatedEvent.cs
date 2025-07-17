using ProductCatalogue.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogue.Domain.Events.ProductEvents
{
    public class ProductPriceUpdatedEvent : DomainEvent
    {
        public int ProductId { get; }
        public decimal UpdatedPrice { get; }
        public ProductPriceUpdatedEvent(int productId, decimal updatedPrice) 
        {
            ProductId = productId;
            UpdatedPrice = updatedPrice;
        }
    }
}
