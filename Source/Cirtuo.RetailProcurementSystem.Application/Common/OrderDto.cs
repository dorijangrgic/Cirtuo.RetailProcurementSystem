namespace Cirtuo.RetailProcurementSystem.Application.Common;

public class OrderDto
{
    public int Id { get; private set; }
    public int RetailerId { get; private set; }
    public DateTime OrderDate { get; private set; }
    public DateTime? DeliveryDate { get; private set; }
    public DateTime? PaymentDate { get; private set; }
    public decimal TotalPrice { get; private set; }
    
    public RetailerDto Retailer { get; private set; }
    public IEnumerable<OrderItemDto> OrderItems { get; private set; }

    public OrderDto(
        int id,
        int retailerId,
        DateTime orderDate,
        DateTime? deliveryDate,
        DateTime? paymentDate,
        decimal totalPrice
    )
    {
        Id = id;
        RetailerId = retailerId;
        OrderDate = orderDate;
        DeliveryDate = deliveryDate;
        PaymentDate = paymentDate;
        TotalPrice = totalPrice;
    }
}