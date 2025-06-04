using MediatR;
using ProductManagement.Core.CustomExceptions;
using ProductManagement.Core.DAL;

namespace ProductManagement.Core.Commands.Handlers
{
    internal sealed class DeleteProductHandler : IRequestHandler<DeleteProduct, bool>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(DeleteProduct request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
            if (product is null)
            {
                throw new ProductNotFoundException(request.Id);
            }

            return await _productRepository.DeleteAsync(product, cancellationToken);
        }
    }
}
