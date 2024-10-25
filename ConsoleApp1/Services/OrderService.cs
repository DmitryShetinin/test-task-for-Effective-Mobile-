



namespace DeliveryService;
public class OrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly string _outPath;
    private readonly FileLogger _fileLogger;
    public OrderService(IOrderRepository orderRepository, string outPath, FileLogger fileLogger)
    {
        _orderRepository = orderRepository;
        _outPath = outPath;
        _fileLogger = fileLogger;
    }



    public async Task<List<Order>?> FilterOrdersAsync(DateTime startDate, string district)
    {
        try
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            DateTime endTime = startDate.AddMinutes(30);
            var filteredOrders = orders.Where(order =>
                                               order.District == district &&
                                               order.DeliveryTime >= startDate &&
                                               order.DeliveryTime <= endTime).ToList();



            await _orderRepository.SaveFilteredOrdersAsync(filteredOrders, _outPath);

            return filteredOrders;
        }
        catch (Exception ex)
        {
            await _fileLogger.LogError($"{ex.Message}");
        }

        return null;
    }


}
