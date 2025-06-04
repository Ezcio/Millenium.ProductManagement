using Moq;
using ProductManagement.Core.Commands.Handlers;
using ProductManagement.Core.Commands;
using ProductManagement.Core.DAL;
using ProductManagement.Core.DomainModels;
using ProductManagement.Core.Enums;

namespace ProductManagement.Tests.UnitTests
{
    public class AddProductHandlerTests
    {
        [Fact]
        public async Task Handle_ValidProduct_ReturnsCreatedProductId()
        {
            var mockRepo = new Mock<IProductRepository>();
            var handler = new AddProductHandler(mockRepo.Object);

            var request = new AddProduct("Test Product", 49.99m, ProductType.Standard);

            var createdProduct = new Product(request.Name, request.Price, request.ProductType);

            createdProduct.SetId(213);

            mockRepo
                .Setup(repo => repo.CreateAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(createdProduct);

            var resultId = await handler.Handle(request, CancellationToken.None);

            Assert.Equal(213, resultId);
        }
    }

}
