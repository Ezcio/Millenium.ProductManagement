using MediatR;
using ProductManagement.Core.DAL;
using ProductManagement.Core.DomainModels;
using System.ComponentModel;

namespace ProductManagement.Core.Commands.Handlers
{
    internal sealed class AddProductHandler : IRequestHandler<AddProduct, int>
    {
        private readonly IProductRepository _productRepository;

        public AddProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<int> Handle(AddProduct request, CancellationToken cancellationToken)
        {
            var product = new Product(request.Name, request.Price, request.ProductType);
            var addedProduct = await _productRepository.CreateAsync(product, cancellationToken);
            return addedProduct.Id;
        }
    }
}
