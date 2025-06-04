using MediatR;
using ProductManagement.Core.DTO;

namespace ProductManagement.Core.Queries
{
    public record GetProducts() : IRequest<IEnumerable<ProductDto>>;
}
