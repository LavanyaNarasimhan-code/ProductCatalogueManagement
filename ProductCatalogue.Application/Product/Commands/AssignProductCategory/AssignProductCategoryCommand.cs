using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogue.Application.Product.Commands
{
    public record AssignProductCategoryCommand(int ProductId, int CategoryId) : IRequest;
}
