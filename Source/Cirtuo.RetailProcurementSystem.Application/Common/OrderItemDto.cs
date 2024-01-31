using Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems;
using Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Models;

namespace Cirtuo.RetailProcurementSystem.Application.Common;

public class OrderItemDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int SupplierStoreItemId { get; set; }
    public int Quantity { get; set; }
    public decimal ItemPrice { get; set; }
    
    public OrderDto Order { get; set; }
    public SupplierStoreItemDto SupplierStoreItem { get; set; }

    public OrderItemDto(int id, int orderId, int supplierStoreItemId, int quantity, decimal itemPrice)
    {
        Id = id;
        OrderId = orderId;
        SupplierStoreItemId = supplierStoreItemId;
        Quantity = quantity;
        ItemPrice = itemPrice;
    }
}