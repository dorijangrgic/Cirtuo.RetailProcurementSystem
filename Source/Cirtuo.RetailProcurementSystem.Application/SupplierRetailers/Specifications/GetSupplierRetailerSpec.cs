using Ardalis.Specification;
using Cirtuo.RetailProcurementSystem.Domain;

namespace Cirtuo.RetailProcurementSystem.Application.SupplierRetailers.Specifications;

public sealed class GetSupplierRetailerSpec : Specification<SupplierRetailer>
{
    public GetSupplierRetailerSpec(int supplierId, int retailerId)
    {
        Query
            .AsTracking()
            .Where(x => x.SupplierId == supplierId && x.RetailerId == retailerId);
    }
}