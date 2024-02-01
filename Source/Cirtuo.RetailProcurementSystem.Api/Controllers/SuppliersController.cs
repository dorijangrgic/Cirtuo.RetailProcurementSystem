using Cirtuo.RetailProcurementSystem.Application.Suppliers.Models;
using Cirtuo.RetailProcurementSystem.Application.Suppliers.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cirtuo.RetailProcurementSystem.Api.Controllers;

[ApiController]
[Route("api/suppliers")]
public class SuppliersController : ControllerBase
{
    private readonly ISupplierService _supplierService;

    public SuppliersController(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetSuppliers(CancellationToken cancellationToken)
    {
        var suppliers = await _supplierService.GetSuppliersAsync(cancellationToken);
        return Ok(suppliers);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSupplierDetails(int id, CancellationToken cancellationToken)
    {
        var supplier = await _supplierService.GetSupplierAsync(id, cancellationToken);
        return Ok(supplier);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateSupplier(SupplierDto supplierDto, CancellationToken cancellationToken)
    {
        var id = await _supplierService.CreateSupplierAsync(supplierDto, cancellationToken);
        return Created($"suppliers/{id}", null);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSupplier(int id, SupplierDto supplierDto, CancellationToken cancellationToken)
    {
        await _supplierService.UpdateSupplierAsync(id, supplierDto, cancellationToken);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSupplier(int id, CancellationToken cancellationToken)
    {
        await _supplierService.DeleteSupplierAsync(id, cancellationToken);
        return NoContent();
    }
}