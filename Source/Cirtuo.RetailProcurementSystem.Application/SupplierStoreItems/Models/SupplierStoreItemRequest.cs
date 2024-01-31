namespace Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Models;

public class SupplierStoreItemRequest
{
    public int SupplierId { get; set; }
    public int StoreItemId { get; set; }
    
    public SupplierStoreItemRequest(int supplierId, int storeItemId)
    {
        SupplierId = supplierId;
        StoreItemId = storeItemId;
    }
}