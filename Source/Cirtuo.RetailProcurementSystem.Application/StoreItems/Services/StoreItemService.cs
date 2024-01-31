using Cirtuo.RetailProcurementSystem.Application.StoreItems.Models;
using Cirtuo.RetailProcurementSystem.Application.StoreItems.Specifications;
using Cirtuo.RetailProcurementSystem.Domain;

namespace Cirtuo.RetailProcurementSystem.Application.StoreItems.Services;

public class StoreItemService : IStoreItemService
{
    private readonly IGenericRepository<StoreItem> _storeItemRepository;

    public StoreItemService(IGenericRepository<StoreItem> storeItemRepository)
    {
        _storeItemRepository = storeItemRepository;
    }

    public async Task<IEnumerable<StoreItemDto>> GetStoreItemsAsync()
    {
        var storeItems = await _storeItemRepository.ListAsync();
        return storeItems.Select(storeItem => new StoreItemDto(
            storeItem.Id,
            storeItem.Sku,
            storeItem.Name,
            storeItem.Description,
            (StoreItemCategoryDto)storeItem.Category
        ));
    }

    public async Task<StoreItemDto> GetStoreItemAsync(int id)
    {
        var storeItem = await _storeItemRepository.GetByIdAsync(id);
        
        if (storeItem == null) throw new ApplicationException($"Store item with id {id} does not exist.");

        return new StoreItemDto(
            storeItem.Id,
            storeItem.Sku,
            storeItem.Name,
            storeItem.Description,
            (StoreItemCategoryDto)storeItem.Category
        );
    }

    public async Task<int> CreateStoreItemAsync(StoreItemDto storeItemDto)
    {
        var storeItem = new StoreItem(
            storeItemDto.Sku,
            storeItemDto.Name,
            storeItemDto.Description,
            (StoreItemCategory)storeItemDto.Category
        );
        await _storeItemRepository.AddAsync(storeItem);
        await _storeItemRepository.SaveChangesAsync();
        return storeItem.Id;
    }

    public async Task UpdateStoreItemAsync(int id, StoreItemDto storeItemDto)
    {
        var storeItem = await _storeItemRepository.GetByIdAsync(id);
        if (storeItem is null) throw new ApplicationException($"Store item with id {id} does not exist.");
        
        storeItem.Update(storeItemDto.Sku, storeItemDto.Name, storeItemDto.Description, (StoreItemCategory)storeItemDto.Category);
        await _storeItemRepository.UpdateAsync(storeItem);
        await _storeItemRepository.SaveChangesAsync();
    }

    public async Task DeleteStoreItemAsync(int id)
    {
        var storeItem = await _storeItemRepository.GetByIdAsync(id);
        if (storeItem is null) throw new ApplicationException($"Store item with id {id} does not exist.");
        
        await _storeItemRepository.DeleteAsync(storeItem);
        await _storeItemRepository.SaveChangesAsync();
    }
}