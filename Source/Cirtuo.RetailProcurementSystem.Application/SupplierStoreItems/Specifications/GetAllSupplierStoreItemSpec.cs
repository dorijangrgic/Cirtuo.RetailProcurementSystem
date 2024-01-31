using Ardalis.Specification;
using Cirtuo.RetailProcurementSystem.Domain;

namespace Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Specifications;

public sealed class GetAllSupplierStoreItemSpec : Specification<SupplierStoreItem>
{
    public GetAllSupplierStoreItemSpec()
    {
        Query
            .Take(50)
            .Include(x => x.StoreItem)
            .Include(x => x.Supplier)
            .Include(x => x.Supplier.Contact)
            .Include(x => x.Supplier.Location);
    }
}