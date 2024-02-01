using Cirtuo.RetailProcurementSystem.Application.StoreItems.Models;

namespace Cirtuo.RetailProcurementSystem.Application.StoreItems.Services;

public interface IStoreItemService
{
    Task<IEnumerable<StoreItemDto>> GetStoreItemsAsync(CancellationToken cancellationToken);
    Task<StoreItemDto> GetStoreItemAsync(int id, CancellationToken cancellationToken);
    Task<int> CreateStoreItemAsync(StoreItemDto storeItemDto, CancellationToken cancellationToken);
    Task UpdateStoreItemAsync(int id, StoreItemDto storeItemDto, CancellationToken cancellationToken);
    Task DeleteStoreItemAsync(int id, CancellationToken cancellationToken);
}