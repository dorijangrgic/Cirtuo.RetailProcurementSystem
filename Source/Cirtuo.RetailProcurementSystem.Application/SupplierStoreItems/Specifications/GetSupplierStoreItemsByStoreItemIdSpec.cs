using Ardalis.Specification;
using Cirtuo.RetailProcurementSystem.Domain;

namespace Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Specifications;

public sealed class GetSupplierStoreItemsByStoreItemIdSpec : Specification<SupplierStoreItem>
{
    public GetSupplierStoreItemsByStoreItemIdSpec(int storeItemId)
    {
        Query
            .Include(x => x.StoreItem)
            .Include(x => x.Supplier)
            .Include(x => x.Supplier.Contact)
            .Include(x => x.Supplier.Location)
            .Where(x => x.StoreItemId == storeItemId)
            .OrderBy(x => x.ItemPrice);
    }
}