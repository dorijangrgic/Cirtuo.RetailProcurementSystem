using Cirtuo.RetailProcurementSystem.Application.StoreItems;
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
    public async Task<IActionResult> GetStoreItems()
    {
        var storeItems = await _storeItemService.GetStoreItemsAsync();
        return Ok(storeItems);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetStoreItemDetails(int id)
    {
        var storeItem = await _storeItemService.GetStoreItemAsync(id);
        return Ok(storeItem);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateStoreItem(StoreItemDto storeItemDto)
    {
        var id = await _storeItemService.CreateStoreItemAsync(storeItemDto);
        return Created($"store-items/{id}", null);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStoreItem(int id, StoreItemDto storeItemDto)
    {
        await _storeItemService.UpdateStoreItemAsync(id, storeItemDto);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStoreItem(int id)
    {
        await _storeItemService.DeleteStoreItemAsync(id);
        return NoContent();
    }
}