using Core.Entities.OrderAggregate;

namespace API.Dtos;

public class OrderToReturnDto
{
    public Guid Id { get; set; }
    public string BuyerEmail { get; set; }
    public DateTime OrderData { get; set; } 
    public IReadOnlyList<OrderItemDto> OrderItems { get; set; }
    public Address ShipToAddress { get; set; }
    public Guid DeliveryMethod { get; set; }
    public decimal ShippingPrice { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Total { get; set; }
    public string Status { get; set; }
}
