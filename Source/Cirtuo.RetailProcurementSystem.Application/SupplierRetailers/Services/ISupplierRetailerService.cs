using Cirtuo.RetailProcurementSystem.Application.SupplierRetailers.Models;
using Cirtuo.RetailProcurementSystem.Application.Suppliers.Models;

namespace Cirtuo.RetailProcurementSystem.Application.SupplierRetailers.Services;

public interface ISupplierRetailerService
{
    Task AddSuppliersForUpcomingQuarterAsync(ConnectSupplierRetailerRequest connectSupplierRetailerRequest, CancellationToken cancellationToken);
    Task<IEnumerable<SupplierDto>> GetSuppliersForCurrentQuarterAsync(CancellationToken cancellationToken);
}