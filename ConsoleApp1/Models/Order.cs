

namespace DeliveryService;

public class Order
{
    public required string? Id { get; set; }
    public double Weight { get; set; }
    public required string District { get; set; }
    public DateTime DeliveryTime { get; set; }
}
