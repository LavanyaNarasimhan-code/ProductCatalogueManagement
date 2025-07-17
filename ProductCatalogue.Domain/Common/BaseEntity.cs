using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProductCatalogue.Domain.Events;


namespace ProductCatalogue.Domain.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; protected set; }
    }
}
