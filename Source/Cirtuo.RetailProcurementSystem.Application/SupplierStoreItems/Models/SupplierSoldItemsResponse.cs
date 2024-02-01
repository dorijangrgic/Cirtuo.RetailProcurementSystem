using Cirtuo.RetailProcurementSystem.Application.Suppliers.Models;

namespace Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Models;

public class SupplierSoldItemsResponse
{
    public SupplierDto Supplier { get; set; }
    public int ItemsCount { get; set; }
    
    public SupplierSoldItemsResponse(SupplierDto supplier, int itemsCount)
    {
        Supplier = supplier;
        ItemsCount = itemsCount;
    }
}