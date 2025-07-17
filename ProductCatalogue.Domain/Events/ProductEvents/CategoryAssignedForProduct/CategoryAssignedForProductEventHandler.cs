using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogue.Domain.Events.ProductEvents
{
    public class CategoryAssignedForProductEventHandler : INotificationHandler<CategoryAssignedForProductEvent>
    {
        public Task Handle(CategoryAssignedForProductEvent notification, CancellationToken cancellationToken)
        {
            // Logic to handle the event when a category is assigned to a product
            // This could include logging, updating other systems, etc.
            Console.WriteLine($"Category {notification.CategoryId} assigned to Product {notification.ProductId} at {notification.OccurredOn}");
            return Task.CompletedTask;
        }
    }
}
