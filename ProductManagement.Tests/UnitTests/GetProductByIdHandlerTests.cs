using Moq;
using ProductManagement.Core.DAL;
using ProductManagement.Core.DomainModels;
using ProductManagement.Core.Queries;
using ProductManagement.Core.Queries.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Tests.UnitTests
{
    public class GetProductByIdHandlerTests
    {
        [Fact]
        public async Task Handle_ProductExists_ReturnsProductDto()
        {
            var mockRepo = new Mock<IProductRepository>();
            var handler = new GetProductByIdHandler(mockRepo.Object);

            var request = new GetProductById(1);
            var product = new Product(1, "Test product", 99.99m, Core.Enums.ProductType.Standard);

            mockRepo.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(product);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(product.Id, result!.Id);
            Assert.Equal(product.Name, result.Name);
            Assert.Equal(product.Price, result.Price);
        }

        [Fact]
        public async Task Handle_ProductNotFound_ReturnsNull()
        {
            var mockRepo = new Mock<IProductRepository>();
            var handler = new GetProductByIdHandler(mockRepo.Object);

            var request = new GetProductById(999);

            mockRepo.Setup(r => r.GetByIdAsync(999, It.IsAny<CancellationToken>()))
                    .ReturnsAsync((Product?)null);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.Null(result);
        }
    }

}
