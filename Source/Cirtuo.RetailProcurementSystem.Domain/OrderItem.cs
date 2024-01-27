namespace Cirtuo.RetailProcurementSystem.Domain;

public class OrderItem
{
    public int Id { get; }
    public int OrderId { get; private set; }
    public int SupplierStoreItemId { get; private set; }
    public int Quantity { get; private set; }
    public decimal ItemPrice { get; private set; }
    
    public Order Order { get; private set; }
    public SupplierStoreItem SupplierStoreItem { get; private set; }
    
    public OrderItem(int id, int orderId, int supplierStoreItemId, int quantity, decimal itemPrice)
    {
        Id = id;
        OrderId = orderId;
        SupplierStoreItemId = supplierStoreItemId;
        Quantity = quantity;
        ItemPrice = itemPrice;
    }
}