using System.Text.Json;

namespace DeliveryService;

public class OrderRepository : IOrderRepository
{
    private List<Order> _orders;
    private readonly FileLogger _fileLogger;
    private readonly OrderValidationContext _orderValidationContext;
    private readonly string _filePath;
    public OrderRepository(FileLogger fileLogger, string filePath)
    {
        _orders = new List<Order>();
        _fileLogger = fileLogger;
        _filePath = filePath;
        _orderValidationContext = new OrderValidationContext();


        _orderValidationContext.AddValidator(new DateValidator());
        _orderValidationContext.AddValidator(new RequiredFieldsValidator());

    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        try
        {
            using (FileStream fs = new FileStream(_filePath, FileMode.OpenOrCreate))
            {
                _orders = await JsonSerializer.DeserializeAsync<List<Order>>(fs) ?? new List<Order>();

                if (!_orderValidationContext.Validate(_orders, _fileLogger))
                {
                    throw new InvalidOperationException("Данные невалидны");
                }

                await _fileLogger.LogInfo("Данные получены");
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Ошибка при загрузке заказов: {ex.Message}");
        }

        return _orders;
    }



    public async Task SaveFilteredOrdersAsync(List<Order> orders, string filePath)
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                await JsonSerializer.SerializeAsync(fs, orders, options);
                await _fileLogger.LogInfo("Данные сохранены");
            }
        }
        catch (Exception ex)
        {
            await _fileLogger.LogError("Ошибка при сохранении заказов:", ex);
            throw;
        }
    }




}
