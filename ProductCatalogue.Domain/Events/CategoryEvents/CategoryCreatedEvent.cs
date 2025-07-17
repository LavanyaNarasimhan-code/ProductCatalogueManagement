using ProductCatalogue.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogue.Domain.Events.CategoryEvents
{
    public class CategoryCreatedEvent : DomainEvent
    {
        public Category Category { get; }
        public CategoryCreatedEvent(Category category) 
        {
            Category = category;
        }
    }
}
