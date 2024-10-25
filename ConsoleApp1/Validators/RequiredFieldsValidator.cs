
namespace DeliveryService;

public class RequiredFieldsValidator : IValidator<Order>
{
    public bool Validate(Order order)
    {
        return !string.IsNullOrEmpty(order.Id) 
                && !string.IsNullOrEmpty(order.DeliveryTime.ToString()) 
                && !string.IsNullOrEmpty(order.District);
    }
}

public class DateValidator : IValidator<Order>
{
    public bool Validate(Order order)
    {
        return DateTime.TryParse(order.DeliveryTime.ToString(), out _) 
                && order.DeliveryTime >= DateTime.Now ;
    }
}


public class InputDataValidator : IValidator<string[]>
{
    public bool Validate(string[] inputData)
    {
         if (inputData.Length < 3)
        {
            return false;
        }

        bool _cityDistrict = inputData[0] != string.Empty;
        bool _firstDeliveryDateTime = DateTime.TryParse(inputData[1], out _);
        bool _deliveryLog = inputData[2] != string.Empty; 
        bool _deliveryOrder = inputData[3] != string.Empty; 


        if(!_firstDeliveryDateTime){
            Console.WriteLine($"дата _firstDeliveryDateTime некорректна");
        }
        
        if(!_cityDistrict){
            Console.WriteLine($"район _cityDistrict некорректен");
        }
        
        if(!_deliveryLog){
            Console.WriteLine($"путь до _deliveryLog некорректен");
        }

        if(!_deliveryOrder){
            Console.WriteLine($"путь до _deliveryLog некорректен");
        }
        bool resultValidation = _cityDistrict 
                                    && _firstDeliveryDateTime 
                                    && _deliveryLog 
                                    && _deliveryOrder;  
        return resultValidation;
    }
}


 

 
 