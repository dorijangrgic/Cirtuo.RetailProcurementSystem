using Cirtuo.RetailProcurementSystem.Application.Common;
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

    public async Task<IEnumerable<StoreItemDto>> GetStoreItemsAsync(CancellationToken cancellationToken)
    {
        var storeItems = await _storeItemRepository.ListAsync(cancellationToken);
        return storeItems.Select(storeItem => new StoreItemDto(
            storeItem.Id,
            storeItem.Sku,
            storeItem.Name,
            storeItem.Description,
            (StoreItemCategoryDto)storeItem.Category
        ));
    }

    public async Task<StoreItemDto> GetStoreItemAsync(int id, CancellationToken cancellationToken)
    {
        var storeItem = await _storeItemRepository.GetByIdAsync(id, cancellationToken);
        
        if (storeItem == null) throw new NotFoundException($"Store item with id {id} does not exist.");

        return new StoreItemDto(
            storeItem.Id,
            storeItem.Sku,
            storeItem.Name,
            storeItem.Description,
            (StoreItemCategoryDto)storeItem.Category
        );
    }

    public async Task<int> CreateStoreItemAsync(StoreItemDto storeItemDto, CancellationToken cancellationToken)
    {
        var storeItem = new StoreItem(
            storeItemDto.Sku,
            storeItemDto.Name,
            storeItemDto.Description,
            (StoreItemCategory)storeItemDto.Category
        );
        await _storeItemRepository.AddAsync(storeItem, cancellationToken);
        await _storeItemRepository.SaveChangesAsync(cancellationToken);
        return storeItem.Id;
    }

    public async Task UpdateStoreItemAsync(int id, StoreItemDto storeItemDto, CancellationToken cancellationToken)
    {
        var storeItem = await _storeItemRepository.GetByIdAsync(id, cancellationToken);
        if (storeItem is null) throw new NotFoundException($"Store item with id {id} does not exist.");
        
        storeItem.Update(storeItemDto.Sku, storeItemDto.Name, storeItemDto.Description, (StoreItemCategory)storeItemDto.Category);
        await _storeItemRepository.UpdateAsync(storeItem, cancellationToken);
        await _storeItemRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteStoreItemAsync(int id, CancellationToken cancellationToken)
    {
        var storeItem = await _storeItemRepository.GetByIdAsync(id, cancellationToken);
        if (storeItem is null) throw new NotFoundException($"Store item with id {id} does not exist.");
        
        await _storeItemRepository.DeleteAsync(storeItem, cancellationToken);
        await _storeItemRepository.SaveChangesAsync(cancellationToken);
    }
}