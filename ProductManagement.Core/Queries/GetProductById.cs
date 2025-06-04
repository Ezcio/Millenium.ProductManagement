using MediatR;
using ProductManagement.Core.DomainModels;
using ProductManagement.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Core.Queries
{
    public record GetProductById(int id) : IRequest<ProductDto>;
}
