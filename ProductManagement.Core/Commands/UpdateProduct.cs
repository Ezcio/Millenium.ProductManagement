using MediatR;
using ProductManagement.Core.DomainModels;
using ProductManagement.Core.DTO;
using ProductManagement.Core.Enums;

namespace ProductManagement.Core.Commands
{
    public record UpdateProduct(int Id, string Name, decimal Price, ProductType ProductType) : IRequest<bool>;
}
