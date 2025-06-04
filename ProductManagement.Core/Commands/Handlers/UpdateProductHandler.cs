using MediatR;
using ProductManagement.Core.CustomExceptions;
using ProductManagement.Core.DAL;
using ProductManagement.Core.DomainModels;
using ProductManagement.Core.DTO;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Core.Commands.Handlers
{
    internal sealed class UpdateProductHandler : IRequestHandler<UpdateProduct, bool>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(UpdateProduct request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
            if (product is null)
            {
                throw new ProductNotFoundException(request.Id);
            }

            return await _productRepository.UpdateAsync(product, cancellationToken);
        }
    }
}
