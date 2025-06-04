using MediatR;
using ProductManagement.Core.DAL;
using ProductManagement.Core.DTO;

namespace ProductManagement.Core.Queries.Handlers
{
    internal sealed class GetProductsHandler : IRequestHandler<GetProducts, IEnumerable<ProductDto>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetProducts request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync(cancellationToken);
            if (products is not null &&
                !products.Any())
            {
                return Enumerable.Empty<ProductDto>();
            }

            var productsDto = products.Select(x => new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                ProductType = x.ProductType,
            });

            return productsDto;
        }
    }
}
