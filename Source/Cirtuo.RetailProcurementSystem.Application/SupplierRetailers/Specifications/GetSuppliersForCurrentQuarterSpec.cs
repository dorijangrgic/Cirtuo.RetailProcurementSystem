using Ardalis.Specification;
using Cirtuo.RetailProcurementSystem.Domain;

namespace Cirtuo.RetailProcurementSystem.Application.SupplierRetailers.Specifications;

public sealed class GetSuppliersForCurrentQuarterSpec : Specification<SupplierRetailer>
{
    public GetSuppliersForCurrentQuarterSpec(DateTime endDate)
    {
        Query
            .Include(x => x.Supplier)
            .Include(x => x.Supplier.Location)
            .Include(x => x.Supplier.Contact)
            .Where(x => x.EndDate >= endDate);
    }
}