using Ardalis.Specification;
using Cirtuo.RetailProcurementSystem.Domain;

namespace Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Specifications;

public sealed class GetSupplierStoreItemForConnectionSpec : Specification<SupplierStoreItem>
{
    public GetSupplierStoreItemForConnectionSpec(int supplierId, int storeItemId, int quarter, int year)
    {
        Query.Where(x => x.StoreItemId == storeItemId && x.SupplierId == supplierId && x.Quarter == quarter && x.Year == year);
    }
}