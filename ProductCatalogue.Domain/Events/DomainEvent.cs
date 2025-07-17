using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogue.Domain.Events
{
    public abstract class DomainEvent : INotification
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;       
    }
}
