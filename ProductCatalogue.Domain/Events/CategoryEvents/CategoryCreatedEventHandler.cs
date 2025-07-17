using MediatR;

namespace ProductCatalogue.Domain.Events.CategoryEvents
{
    public class CategoryCreatedEventHandler : INotificationHandler<CategoryCreatedEvent>
    {
        public Task Handle(CategoryCreatedEvent notification, CancellationToken cancellationToken)
        {
            // Logic to handle the category created event
            // For example, logging or updating a cache
            Console.WriteLine($"Category Created at {notification.OccurredOn} - CategoryName: {notification.Category.CategoryName} with ID {notification.Category.Id}");
            return Task.CompletedTask;
        }
    }
}
