
namespace DeliveryService;

public interface IOrderRepository
{
 
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task SaveFilteredOrdersAsync(List<Order> orders, string filePath);
}
