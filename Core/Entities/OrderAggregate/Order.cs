namespace Core.Entities.OrderAggregate;

public class Order : BaseEntity
{
    public string BuyerEmail { get; set; }
    public DateTime OrderData { get; set; } = DateTime.UtcNow;
    public IReadOnlyList<OrderItem> OrderItems { get; set; }
    public Address ShipToAddress { get; set; }
    public DeliveryMethod DeliveryMethod { get; set; }
    public decimal Subtotal { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public string PaymentIntentId { get; set; }

    public Order()
    {
    }

    public Order(IReadOnlyList<OrderItem> orderItems, string buyerEmail, Address shipToAddress,
        DeliveryMethod deliveryMethod, decimal subtotal, string paymentIntentId)
    {
        BuyerEmail = buyerEmail;
        OrderItems = orderItems;
        ShipToAddress = shipToAddress;
        DeliveryMethod = deliveryMethod;
        Subtotal = subtotal;
        PaymentIntentId = paymentIntentId;
    }


    public decimal GetTotal()
    {
        return Subtotal + DeliveryMethod.Price;
    }
}
