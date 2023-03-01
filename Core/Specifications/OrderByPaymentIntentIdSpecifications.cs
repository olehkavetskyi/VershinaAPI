using Core.Entities.OrderAggregate;

namespace Core.Specifications;

public class OrderByPaymentIntentIdSpecifications : BaseSpecification<Order>
{
    public OrderByPaymentIntentIdSpecifications(string paymentIntentId) 
        : base(o => o.PaymentIntentId == paymentIntentId)
    {

    }
}
