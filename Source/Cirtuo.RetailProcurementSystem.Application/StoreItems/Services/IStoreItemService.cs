using Cirtuo.RetailProcurementSystem.Application.StoreItems.Models;

namespace Cirtuo.RetailProcurementSystem.Application.StoreItems.Services;

public interface IStoreItemService
{
    Task<IEnumerable<StoreItemDto>> GetStoreItemsAsync();
    Task<StoreItemDto> GetStoreItemAsync(int id);
    Task<int> CreateStoreItemAsync(StoreItemDto storeItemDto);
    Task UpdateStoreItemAsync(int id, StoreItemDto storeItemDto);
    Task DeleteStoreItemAsync(int id);
}