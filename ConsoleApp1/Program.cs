using System.Text.Json;
using ConsoleTables;

namespace DeliveryService;

class Program
{
   

   

    static async Task Main(string[] args)
    {
 
        if(!new InputDataValidator().Validate(args)){
            Console.WriteLine($"Данные невалидны");
            return; 
            
        }

        FileLogger fileLogger = new FileLogger(args[2]);

        await fileLogger.LogInfo("Программа запустилась");
        
 
        var cityDistrict = args[0];
        var _deliveryOrder = args[3];
       

        DateTime.TryParse(args[1], out DateTime _firstDeliveryDateTime);
       

        var orderService = new OrderService(new OrderRepository(fileLogger, "orders.json"),_deliveryOrder, fileLogger);
        
        var orders = await orderService.FilterOrdersAsync(_firstDeliveryDateTime, cityDistrict);
        if(orders == null){
            return;
        }
   
  

        var table = new ConsoleTable("Id", "Weight", "DeliveryTime");
        foreach (var order in orders)
        {
            table.AddRow(order.Id, order.Weight, order.DeliveryTime.ToString());
        }
        table.Write();
    }
}
