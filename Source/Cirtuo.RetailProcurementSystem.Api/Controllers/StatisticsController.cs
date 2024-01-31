using Cirtuo.RetailProcurementSystem.Application.SupplierRetailers.Models;
using Cirtuo.RetailProcurementSystem.Application.SupplierRetailers.Services;
using Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Service;
using Microsoft.AspNetCore.Mvc;

namespace Cirtuo.RetailProcurementSystem.Api.Controllers;

[ApiController]
[Route("api/statistics")]
public class StatisticsController : ControllerBase
{
    private readonly ISupplierStoreItemService _supplierStoreItemService;
    private readonly ISupplierRetailerService _supplierRetailerService;

    public StatisticsController(
        ISupplierStoreItemService supplierStoreItemService,
        ISupplierRetailerService supplierRetailerService
    )
    {
        _supplierStoreItemService = supplierStoreItemService;
        _supplierRetailerService = supplierRetailerService;
    }

    [HttpGet("supplier/{id}")]
    public async Task<IActionResult> GetSupplierSoldItemsCount(int id)
    {
        var count = await _supplierStoreItemService.GetSoldItemsCountAsync(id);
        return Ok(count);
    }
    
    [HttpGet("best-offer/{productId}")]
    public async Task<IActionResult> GetBestProductOffer(int productId)
    {
        var supplierStoreItem = await _supplierStoreItemService.GetLowestItemPriceForProductAsync(productId);
        return Ok(supplierStoreItem);
    }
    
    [HttpPost("quarterly-plan")]
    public async Task<IActionResult> PlanSuppliersForUpcomingQuarter(ConnectSupplierRetailerRequest connectSupplierRetailerRequest)
    {
        await _supplierRetailerService.AddSuppliersForUpcomingQuarterAsync(connectSupplierRetailerRequest);
        return NoContent();
    }
    
    [HttpGet("quarterly-plan")]
    public async Task<IActionResult> GetSuppliersForCurrentQuarter()
    {
        var suppliers = await _supplierRetailerService.GetSuppliersForCurrentQuarterAsync();
        return Ok(suppliers);
    }
}