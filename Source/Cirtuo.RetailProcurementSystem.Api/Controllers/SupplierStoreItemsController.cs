using Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems;
using Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Models;
using Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Service;
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
    public async Task<IActionResult> GetAllSupplierStoreItems()
    {
        var supplierStoreItems = await _supplierStoreItemService.GetSupplierStoreItemsAsync();
        return Ok(supplierStoreItems);
    }
    
    [HttpPost]
    public async Task<IActionResult> ConnectSupplierStoreItem(SupplierStoreItemDto supplierStoreItemRequestDto)
    {
        var id = await _supplierStoreItemService.ConnectSupplierStoreItemAsync(supplierStoreItemRequestDto);
        return Created($"supplier-store-items/{id}", null);
    }
    
    [HttpDelete("{supplierId}/{storeItemId}")]
    public async Task<IActionResult> DisconnectSupplierStoreItem(int supplierId, int storeItemId)
    {
        await _supplierStoreItemService.DisconnectSupplierStoreItemAsync(supplierId, storeItemId);
        return Ok();
    }
}