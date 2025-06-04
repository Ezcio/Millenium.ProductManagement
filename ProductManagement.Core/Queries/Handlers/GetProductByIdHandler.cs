using MediatR;
using ProductManagement.Core.CustomExceptions;
using ProductManagement.Core.DAL;
using ProductManagement.Core.DTO;

namespace ProductManagement.Core.Queries.Handlers
{
    internal sealed class GetProductByIdHandler : IRequestHandler<GetProductById, ProductDto>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto> Handle(GetProductById request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.id, cancellationToken);
            if (product is null)
            {
                return null;
            }

            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ProductType = product.ProductType,
            };

            return productDto;
        }
    }
}
