using ProductCatalogue.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogue.Domain.Events.ProductEvents
{
    public class ProductCreatedEvent : DomainEvent
    {
        public Product Product { get;  }
        public ProductCreatedEvent(Product product)
        {
            Product = product;
        }
    }
}
