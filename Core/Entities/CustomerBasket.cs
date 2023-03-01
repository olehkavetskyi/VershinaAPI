using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Core.Entities;

public class CustomerBasket
{
    public string Id { get; set; }

    public CustomerBasket()
    {
    }

    public CustomerBasket(string id)
    {
        Id = id;
    }

    public List<BasketItem> Items { get; set; } = new();
    public Guid? DeliveryMethodId { get; set; }
    public string? ClientSecret { get; set; }
    public string? PaymentIntentId { get; set; }
    public decimal ShippingPrice { get; set; }
}
