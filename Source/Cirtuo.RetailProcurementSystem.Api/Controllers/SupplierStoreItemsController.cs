using Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Models;
using Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cirtuo.RetailProcurementSystem.Api.Controllers;

[ApiController]
[Route("api/supplier-store-items")]
public class SupplierStoreItemsController : ControllerBase
{
    private readonly ISupplierStoreItemService _supplierStoreItemService;

    public SupplierStoreItemsController(ISupplierStoreItemService supplierStoreItemService)
    {
        _supplierStoreItemService = supplierStoreItemService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllSupplierStoreItems(CancellationToken cancellationToken)
    {
        var supplierStoreItems = await _supplierStoreItemService.GetSupplierStoreItemsAsync(cancellationToken);
        return Ok(supplierStoreItems);
    }
    
    [HttpPost]
    public async Task<IActionResult> ConnectSupplierStoreItem(SupplierStoreItemDto supplierStoreItemRequestDto, CancellationToken cancellationToken)
    {
        var id = await _supplierStoreItemService.ConnectSupplierStoreItemAsync(supplierStoreItemRequestDto, cancellationToken);
        return Created($"supplier-store-items/{id}", null);
    }
    
    [HttpDelete("{supplierId}/{storeItemId}")]
    public async Task<IActionResult> DisconnectSupplierStoreItem(int supplierId, int storeItemId, CancellationToken cancellationToken)
    {
        await _supplierStoreItemService.DisconnectSupplierStoreItemAsync(supplierId, storeItemId, cancellationToken);
        return Ok();
    }
}