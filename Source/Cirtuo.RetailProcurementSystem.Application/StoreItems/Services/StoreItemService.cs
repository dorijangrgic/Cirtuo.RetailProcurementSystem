using AutoMapper;
using Cirtuo.RetailProcurementSystem.Application.Common;
using Cirtuo.RetailProcurementSystem.Application.StoreItems.Models;
using Cirtuo.RetailProcurementSystem.Domain;

namespace Cirtuo.RetailProcurementSystem.Application.StoreItems.Services;

public class StoreItemService : IStoreItemService
{
    private readonly IGenericRepository<StoreItem> _storeItemRepository;
    private readonly IMapper _mapper;

    public StoreItemService(IGenericRepository<StoreItem> storeItemRepository, IMapper mapper)
    {
        _storeItemRepository = storeItemRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<StoreItemDto>> GetStoreItemsAsync(CancellationToken cancellationToken)
    {
        var storeItems = await _storeItemRepository.ListAsync(cancellationToken);
        
        return _mapper.Map<IEnumerable<StoreItemDto>>(storeItems);
    }

    public async Task<StoreItemDto> GetStoreItemAsync(int id, CancellationToken cancellationToken)
    {
        var storeItem = await _storeItemRepository.GetByIdAsync(id, cancellationToken);
        
        if (storeItem == null) throw new NotFoundException($"Store item with id {id} does not exist.");

        return _mapper.Map<StoreItemDto>(storeItem);
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