using Ardalis.Specification;
using Cirtuo.RetailProcurementSystem.Domain;

namespace Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Specifications;

public sealed class GetSupplierStoreItemSpec : Specification<SupplierStoreItem>
{
    public GetSupplierStoreItemSpec(int supplierId, int storeItemId)
    {
        Query.Where(x => x.StoreItemId == storeItemId && x.SupplierId == supplierId);
    }
}