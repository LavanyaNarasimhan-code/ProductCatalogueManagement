using ProductCatalogue.Domain.Common;
using ProductCatalogue.Domain.Events;
using ProductCatalogue.Domain.Events.CategoryEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogue.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }               
        public DateTime CreatedDate { get; set; }

        public Category(string categoryName)
        {
            CategoryName = categoryName;
            CreatedDate = DateTime.UtcNow;
        }
    }
}
