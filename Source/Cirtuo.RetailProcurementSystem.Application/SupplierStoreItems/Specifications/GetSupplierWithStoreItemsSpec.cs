using Ardalis.Specification;
using Cirtuo.RetailProcurementSystem.Domain;

namespace Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Specifications;

public sealed class GetSupplierWithStoreItemsSpec : Specification<Supplier>
{
    public GetSupplierWithStoreItemsSpec(int supplierId)
    {
        Query
            .Where(x => x.Id == supplierId)
            .Include(x => x.SupplierStoreItems);
    }
}