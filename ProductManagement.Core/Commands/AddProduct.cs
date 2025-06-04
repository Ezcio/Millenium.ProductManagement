using MediatR;
using ProductManagement.Core.Enums;

namespace ProductManagement.Core.Commands
{
    public record class AddProduct(string Name, decimal Price, ProductType ProductType) : IRequest<int>;
}
