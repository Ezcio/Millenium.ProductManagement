using Moq;
using ProductManagement.Core.Commands.Handlers;
using ProductManagement.Core.Commands;
using ProductManagement.Core.CustomExceptions;
using ProductManagement.Core.DAL;
using ProductManagement.Core.DomainModels;
using ProductManagement.Core.Enums;

namespace ProductManagement.Tests.UnitTests
{
    public class DeleteProductHandlerTests
    {
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly DeleteProductHandler _handler;

        public DeleteProductHandlerTests()
        {
            _mockRepo = new Mock<IProductRepository>();
            _handler = new DeleteProductHandler(_mockRepo.Object);
        }

        [Fact]
        public async Task Handle_ProductExists_DeletesProductAndReturnsTrue()
        {
            var product = new Product(1, "Sample", 99.99m, ProductType.Standard);

            _mockRepo.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(product);

            _mockRepo.Setup(r => r.DeleteAsync(product, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(true);

            var request = new DeleteProduct(1);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.True(result);
            _mockRepo.Verify(r => r.DeleteAsync(product, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ProductDoesNotExist_ThrowsProductNotFoundException()
        {
            _mockRepo.Setup(r => r.GetByIdAsync(999, It.IsAny<CancellationToken>()))
                     .ReturnsAsync((Product?)null);

            var request = new DeleteProduct(999);
            Action act = async () => await _handler.Handle(request, It.IsAny<CancellationToken>());

            Assert.Throws<ProductNotFoundException>(act);
        }

    }
}
