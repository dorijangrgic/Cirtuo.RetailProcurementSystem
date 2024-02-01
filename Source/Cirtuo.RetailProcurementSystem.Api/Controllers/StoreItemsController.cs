using Cirtuo.RetailProcurementSystem.Application.StoreItems.Models;
using Cirtuo.RetailProcurementSystem.Application.StoreItems.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cirtuo.RetailProcurementSystem.Api.Controllers;

[ApiController]
[Route("api/store-items")]
public class StoreItemsController : ControllerBase
{
    private readonly IStoreItemService _storeItemService;

    public StoreItemsController(IStoreItemService storeItemService)
    {
        _storeItemService = storeItemService;
    }

    [HttpGet]
    public async Task<IActionResult> GetStoreItems(CancellationToken cancellationToken)
    {
        var storeItems = await _storeItemService.GetStoreItemsAsync(cancellationToken);
        return Ok(storeItems);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetStoreItemDetails(int id, CancellationToken cancellationToken)
    {
        var storeItem = await _storeItemService.GetStoreItemAsync(id, cancellationToken);
        return Ok(storeItem);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateStoreItem(StoreItemDto storeItemDto, CancellationToken cancellationToken)
    {
        var id = await _storeItemService.CreateStoreItemAsync(storeItemDto, cancellationToken);
        return Created($"store-items/{id}", null);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStoreItem(int id, StoreItemDto storeItemDto, CancellationToken cancellationToken)
    {
        await _storeItemService.UpdateStoreItemAsync(id, storeItemDto, cancellationToken);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStoreItem(int id, CancellationToken cancellationToken)
    {
        await _storeItemService.DeleteStoreItemAsync(id, cancellationToken);
        return NoContent();
    }
}