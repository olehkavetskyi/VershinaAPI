using Core.Entities.OrderAggregate;

namespace Core.Interfaces;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(string buyerEmail, Guid deliveryMethodId, string basketId,
        Address shippingAddress);
    Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);
    Task<Order> GetOrderByIdAsync(Guid id, string buyerEmail);
    Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
}
