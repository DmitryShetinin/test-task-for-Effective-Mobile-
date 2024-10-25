namespace DeliveryService;

public class OrderValidationContext
{
    private readonly List<IValidator<Order>> _validators;

    public OrderValidationContext()
    {
        _validators = new List<IValidator<Order>>();
    }

    public void AddValidator(IValidator<Order> validator)
    {
        _validators.Add(validator);
    }

    public bool Validate(List<Order> orders, FileLogger fileLogger)
    {
        bool allValid = true;

        foreach (var order in orders)
        {
            foreach (var validator in _validators)
            {
                if (!validator.Validate(order))
                {
                    fileLogger.LogError($"Не удалось выполнить проверку заказа: {order.Id} валидатором: {validator.GetType().Name}");
                    allValid = false; 
                }
            }
        }

        return allValid; 
    }

}
