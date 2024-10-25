 
namespace DeliveryService;

public interface IValidator<T>
{
    bool Validate(T order);
}


