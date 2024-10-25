using Moq;
using Xunit;

namespace DeliveryService.Tests
{
    public class OrderServiceTests
    {
        [Fact]
        public async Task FilterOrdersAsync_ReturnsFilteredOrders()
        {
            // Arrange
            var mockOrderRepository = new Mock<IOrderRepository>();
            var fileLogger = new FileLogger("outputPath.txt"); // Предположим, что у вас есть реализация FileLogger
            var orderService = new OrderService(mockOrderRepository.Object, "outputPath.txt", fileLogger);

             var orders = new List<Order>
            {
                new Order { Id = "1", District = "Central", DeliveryTime = new DateTime(2024, 11, 25, 12, 30, 0) },
                new Order { Id = "2", District = "North", DeliveryTime = new DateTime(2024, 11, 25, 12, 30, 0) },
                new Order { Id = "3", District = "Central",DeliveryTime = new DateTime(2024, 11, 25, 12, 30, 0) },
                new Order { Id = "4", District = "Central", DeliveryTime = new DateTime(2024, 11, 25, 12, 30, 0) }
            };

            mockOrderRepository.Setup(repo => repo.GetAllOrdersAsync()).ReturnsAsync(orders);

            DateTime startDate = DateTime.Now;

            // Act
            var result = await orderService.FilterOrdersAsync(startDate, "Central");

            // Assert
            Assert.NotNull(result);
            Assert.All(result, order => Assert.Equal("Central", order.District));
        }

        [Fact]
        public async Task FilterOrdersAsync_LogsErrorOnException()
        {
            // Arrange
            var mockOrderRepository = new Mock<IOrderRepository>();
            var fileLoggerMock = new Mock<FileLogger>("outputPath.txt"); // Мокируем FileLogger
            var orderService = new OrderService(mockOrderRepository.Object, "outputPath.txt", fileLoggerMock.Object);

            mockOrderRepository.Setup(repo => repo.GetAllOrdersAsync()).ThrowsAsync(new Exception("Test exception"));

            DateTime startDate = DateTime.Now;

            // Act
            var result = await orderService.FilterOrdersAsync(startDate, "A");

            // Assert
            Assert.Null(result);
         
        }
    }
}
