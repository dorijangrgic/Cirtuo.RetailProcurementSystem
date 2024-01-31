using Cirtuo.RetailProcurementSystem.Application.Common;
using Cirtuo.RetailProcurementSystem.Application.Suppliers.Models;
using Cirtuo.RetailProcurementSystem.Application.Suppliers.Specifications;
using Cirtuo.RetailProcurementSystem.Domain;

namespace Cirtuo.RetailProcurementSystem.Application.Suppliers.Services;

public class SupplierService : ISupplierService
{
    private readonly IGenericRepository<Supplier> _supplierRepository;

    public SupplierService(IGenericRepository<Supplier> supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }

    public async Task<IEnumerable<SupplierDto>> GetSuppliersAsync()
    {
        var getSupplierSpec = new GetSupplierSpec();
        var suppliers = await _supplierRepository.ListAsync(getSupplierSpec);
        return suppliers.Select(s =>
        {
            var location = new LocationDto(s.Location.Id, s.Location.Address, s.Location.City, s.Location.State, s.Location.ZipCode);
            var contact = new ContactDto(s.Contact.Id, s.Contact.Email, s.Contact.Phone);
            return new SupplierDto(s.Id, s.Name, location, contact);
        });
    }

    public async Task<SupplierDto> GetSupplierAsync(int id)
    {
        var getSupplierSpec = new GetSupplierSpec(id);
        var supplier = await _supplierRepository.FirstOrDefaultAsync(getSupplierSpec);
        if (supplier is null) throw new ApplicationException($"Supplier with id {id} does not exist.");
        var location = new LocationDto(supplier.Location.Id, supplier.Location.Address, supplier.Location.City, supplier.Location.State, supplier.Location.ZipCode);
        var contact = new ContactDto(supplier.Contact.Id, supplier.Contact.Email, supplier.Contact.Phone);
        return new SupplierDto(supplier.Id, supplier.Name, location, contact);
    }

    public async Task<int> CreateSupplierAsync(SupplierDto supplierDto)
    {
        var supplier = new Supplier(supplierDto.Name, supplierDto.Location.Id, supplierDto.Location.Id);
        await _supplierRepository.AddAsync(supplier);
        await _supplierRepository.SaveChangesAsync();
        return supplier.Id;
    }

    public async Task UpdateSupplierAsync(int id, SupplierDto supplierDto)
    {
        var supplier = await _supplierRepository.GetByIdAsync(id);
        if (supplier is null) throw new ApplicationException($"Supplier with id {id} does not exist.");
        supplier.Update(supplierDto.Name, supplierDto.Location.Id, supplierDto.Contact.Id);
        await _supplierRepository.UpdateAsync(supplier);
        await _supplierRepository.SaveChangesAsync();
    }

    public async Task DeleteSupplierAsync(int id)
    {
        var supplier = await _supplierRepository.GetByIdAsync(id);
        if (supplier is null) throw new ApplicationException($"Supplier with id {id} does not exist.");
        await _supplierRepository.DeleteAsync(supplier);
        await _supplierRepository.SaveChangesAsync();
    }
}