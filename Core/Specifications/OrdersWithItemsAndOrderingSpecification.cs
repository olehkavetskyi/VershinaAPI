using Core.Entities.OrderAggregate;
using System.Linq.Expressions;

namespace Core.Specifications;

public class OrdersWithItemsAndOrderingSpecification : BaseSpecification<Order>
{
    public OrdersWithItemsAndOrderingSpecification(string email) : base(o => o.BuyerEmail == email)
    {
        AddInclude(o => o.OrderItems);
        AddInclude(o => o.DeliveryMethod);
        AddOrderByDescending(o => o.OrderData);
    }

    public OrdersWithItemsAndOrderingSpecification(Guid id, string email) : base(o => o.Id == id && o.BuyerEmail == email)
    {
        AddInclude(o => o.OrderItems);
        AddInclude(o => o.DeliveryMethod);
    }
}
