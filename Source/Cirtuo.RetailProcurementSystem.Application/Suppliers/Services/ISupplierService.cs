using Cirtuo.RetailProcurementSystem.Application.Suppliers.Models;

namespace Cirtuo.RetailProcurementSystem.Application.Suppliers.Services;

public interface ISupplierService
{
    Task<IEnumerable<SupplierDto>> GetSuppliersAsync(CancellationToken cancellationToken);
    Task<SupplierDto> GetSupplierAsync(int id, CancellationToken cancellationToken);
    Task<int> CreateSupplierAsync(SupplierDto supplierDto, CancellationToken cancellationToken);
    Task UpdateSupplierAsync(int id, SupplierDto supplierDto, CancellationToken cancellationToken);
    Task DeleteSupplierAsync(int id, CancellationToken cancellationToken);
}