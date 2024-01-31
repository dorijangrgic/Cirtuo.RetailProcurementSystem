using Cirtuo.RetailProcurementSystem.Application.Suppliers.Models;

namespace Cirtuo.RetailProcurementSystem.Application.Suppliers.Services;

public interface ISupplierService
{
    Task<IEnumerable<SupplierDto>> GetSuppliersAsync();
    Task<SupplierDto> GetSupplierAsync(int id);
    Task<int> CreateSupplierAsync(SupplierDto supplierDto);
    Task UpdateSupplierAsync(int id, SupplierDto supplierDto);
    Task DeleteSupplierAsync(int id);
}