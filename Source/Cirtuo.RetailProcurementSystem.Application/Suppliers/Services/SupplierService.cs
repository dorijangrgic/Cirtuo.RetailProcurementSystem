using AutoMapper;
using Cirtuo.RetailProcurementSystem.Application.Common;
using Cirtuo.RetailProcurementSystem.Application.Suppliers.Models;
using Cirtuo.RetailProcurementSystem.Application.Suppliers.Specifications;
using Cirtuo.RetailProcurementSystem.Domain;

namespace Cirtuo.RetailProcurementSystem.Application.Suppliers.Services;

public class SupplierService : ISupplierService
{
    private readonly IGenericRepository<Supplier> _supplierRepository;
    private readonly IMapper _mapper;

    public SupplierService(IGenericRepository<Supplier> supplierRepository, IMapper mapper)
    {
        _supplierRepository = supplierRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SupplierDto>> GetSuppliersAsync(CancellationToken cancellationToken)
    {
        var getSupplierSpec = new GetSupplierSpec();
        var suppliers = await _supplierRepository.ListAsync(getSupplierSpec, cancellationToken);

        return _mapper.Map<IEnumerable<SupplierDto>>(suppliers);
    }

    public async Task<SupplierDto> GetSupplierAsync(int id, CancellationToken cancellationToken)
    {
        var getSupplierSpec = new GetSupplierSpec(id);
        var supplier = await _supplierRepository.FirstOrDefaultAsync(getSupplierSpec, cancellationToken);
        if (supplier is null) throw new NotFoundException($"Supplier with id {id} does not exist.");
        
        return _mapper.Map<SupplierDto>(supplier);
    }

    public async Task<int> CreateSupplierAsync(SupplierDto supplierDto, CancellationToken cancellationToken)
    {
        var supplier = new Supplier(supplierDto.Name, supplierDto.Location.Id, supplierDto.Location.Id);
        
        await _supplierRepository.AddAsync(supplier, cancellationToken);
        await _supplierRepository.SaveChangesAsync(cancellationToken);
        return supplier.Id;
    }

    public async Task UpdateSupplierAsync(int id, SupplierDto supplierDto, CancellationToken cancellationToken)
    {
        var supplier = await _supplierRepository.GetByIdAsync(id, cancellationToken);
        if (supplier is null) throw new NotFoundException($"Supplier with id {id} does not exist.");
        
        supplier.Update(supplierDto.Name, supplierDto.Location.Id, supplierDto.Contact.Id);
        await _supplierRepository.UpdateAsync(supplier, cancellationToken);
        await _supplierRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteSupplierAsync(int id, CancellationToken cancellationToken)
    {
        var supplier = await _supplierRepository.GetByIdAsync(id, cancellationToken);
        if (supplier is null) throw new NotFoundException($"Supplier with id {id} does not exist.");
        
        await _supplierRepository.DeleteAsync(supplier, cancellationToken);
        await _supplierRepository.SaveChangesAsync(cancellationToken);
    }
}