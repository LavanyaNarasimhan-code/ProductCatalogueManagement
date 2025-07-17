using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogue.Domain.Events.ProductEvents
{
    public class ProductPriceUpdatedEventHandler : INotificationHandler<ProductPriceUpdatedEvent>
    {
        public Task Handle(ProductPriceUpdatedEvent notification, CancellationToken cancellationToken)
        {   
            Console.WriteLine($"Product Price Updated: ProductId = {notification.ProductId}, updatedPrice = {notification.UpdatedPrice} at {notification.OccurredOn}");            
            return Task.CompletedTask;
        }
    }
}
