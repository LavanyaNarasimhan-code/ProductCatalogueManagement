using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogue.Domain.Events.ProductEvents
{
    public class ProductStockUpdatedEventHandler : INotificationHandler<ProductStockUpdatedEvent>
    {
        public async Task Handle(ProductStockUpdatedEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Product stock updated: ProductId = {notification.ProductId}, updatedStock = {notification.UpdateStock} at {notification.OccurredOn}");
            await Task.CompletedTask;
        }
    }
}
