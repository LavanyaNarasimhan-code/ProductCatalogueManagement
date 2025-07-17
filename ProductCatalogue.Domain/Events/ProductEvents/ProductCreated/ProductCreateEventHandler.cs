using MediatR;
using ProductCatalogue.Domain.Events.ProductEvents;

public class ProductCreatedEventHandler : INotificationHandler<ProductCreatedEvent>
{
    public Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Product Created at {notification.OccurredOn} - ProductName: {notification.Product.ProductName}, " +
            $"Price: {notification.Product.Price}, Stock: {notification.Product.Stock}, CategoryId: {notification.Product.CategoryId} with ID: {notification.Product.Id}");
        return Task.CompletedTask;
    }
}
