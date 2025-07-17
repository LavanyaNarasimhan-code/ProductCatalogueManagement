using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCatalogue.Domain.Entities;

namespace ProductCatalogue.Domain.Events.ProductEvents
{
    public class CategoryAssignedForProductEvent : DomainEvent
    {
        public int ProductId { get; }
        public int CategoryId { get; }
        public CategoryAssignedForProductEvent(int ProductId, int CategoryId)
        {
            this.ProductId = ProductId;
            this.CategoryId = CategoryId;
        }
    }
}
