using Ardalis.Specification;
using Cirtuo.RetailProcurementSystem.Domain;

namespace Cirtuo.RetailProcurementSystem.Application.Suppliers.Specifications;

public sealed class GetSupplierSpec : Specification<Supplier>
{
    public GetSupplierSpec(int? id = null)
    {
        Query
            .Include(x => x.Location)
            .Include(x => x.Contact);
        
        if (id is not null) Query.Where(x => x.Id == id);
    }
}