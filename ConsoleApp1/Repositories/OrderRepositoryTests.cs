using System.Text.Json;
using Moq;
using Xunit;

namespace DeliveryService.Tests
{
    public class OrderRepositoryTests
    {
        private readonly Mock<FileLogger> _fileLoggerMock;
        private readonly string _testSaveFilePath;

           private readonly string _testGetFilePath;

        public OrderRepositoryTests()
        {
            _fileLoggerMock = new Mock<FileLogger>("deliveryTest.log");
            _testSaveFilePath = "resultTest.json"; // Путь к тестовому файлу
            _testGetFilePath = "ordersTest.json"; // Путь к тестовому файлу
        }

        [Fact]
        public async Task GetAllOrdersAsync_ValidData_ReturnsOrders()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order { Id = "1", District = "Central", DeliveryTime = new DateTime(2024, 11, 25, 12, 30, 0) },
                new Order { Id = "2", District = "North", DeliveryTime = new DateTime(2024, 11, 25, 12, 30, 0) },
                new Order { Id = "3", District = "Central",DeliveryTime = new DateTime(2024, 11, 25, 12, 30, 0) },
                new Order { Id = "4", District = "Central", DeliveryTime = new DateTime(2024, 11, 25, 12, 30, 0) }
            };

            await File.WriteAllTextAsync(_testGetFilePath, JsonSerializer.Serialize(orders));

            var orderRepository = new OrderRepository(_fileLoggerMock.Object, _testGetFilePath);

            // Act
            var result = await orderRepository.GetAllOrdersAsync();

            // Assert
            Assert.NotNull(result);
        
      
        }
 

        [Fact]
        public async Task SaveFilteredOrdersAsync_ValidData_SavesOrders()
        {
            // Arrange
            var ordersToSave = new List<Order>
            {
                new Order { Id = "1", District = "Central", DeliveryTime = new DateTime(2024, 11, 25, 12, 30, 0) },
                new Order { Id = "2", District = "North", DeliveryTime = new DateTime(2024, 11, 25, 12, 30, 0) },
                new Order { Id = "3", District = "Central",DeliveryTime = new DateTime(2024, 11, 25, 12, 30, 0) },
                new Order { Id = "4", District = "Central", DeliveryTime = new DateTime(2024, 11, 25, 12, 30, 0) }
            };

            var orderRepository = new OrderRepository(_fileLoggerMock.Object, _testSaveFilePath);

            // Act
            await orderRepository.SaveFilteredOrdersAsync(ordersToSave, _testSaveFilePath);

            // Assert
            var savedOrdersJson = await File.ReadAllTextAsync(_testSaveFilePath);
            Console.WriteLine(savedOrdersJson); // Отладочный вывод

            var savedOrders = JsonSerializer.Deserialize<List<Order>>(savedOrdersJson);
            Assert.NotNull(savedOrders);
            Assert.Equal(ordersToSave.Count, savedOrders.Count);
         
        }

    }
}
