namespace Cirtuo.RetailProcurementSystem.Domain;

public class Order
{
    public int Id { get; }
    public int RetailerId { get; private set; }
    public DateTime OrderDate { get; private set; }
    public DateTime? DeliveryDate { get; private set; }
    public DateTime? PaymentDate { get; private set; }
    public decimal TotalPrice { get; private set; }
    
    public Retailer Retailer { get; private set; }
    public ICollection<OrderItem> OrderItems { get; private set; }

    public Order(
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
        
        OrderItems ??= new List<OrderItem>();
    }
}