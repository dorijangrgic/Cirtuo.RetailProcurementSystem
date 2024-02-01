using AutoMapper;
using Cirtuo.RetailProcurementSystem.Application.Common;
using Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Models;
using Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Specifications;
using Cirtuo.RetailProcurementSystem.Domain;

namespace Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Services;

public class SupplierStoreItemService : ISupplierStoreItemService
{
    private readonly IGenericRepository<SupplierStoreItem> _supplierStoreItemRepository;
    private readonly IGenericRepository<Supplier> _supplierRepository;
    private readonly IGenericRepository<StoreItem> _storeItemRepository;
    private readonly IMapper _mapper;

    public SupplierStoreItemService(
        IGenericRepository<SupplierStoreItem> supplierStoreItemRepository,
        IGenericRepository<Supplier> supplierRepository,
        IGenericRepository<StoreItem> storeItemRepository,
        IMapper mapper
    )
    {
        _supplierStoreItemRepository = supplierStoreItemRepository;
        _supplierRepository = supplierRepository;
        _storeItemRepository = storeItemRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SupplierStoreItemDto>> GetSupplierStoreItemsAsync(CancellationToken cancellationToken)
    {
        var getAllSupplierStoreItemSpec = new GetAllSupplierStoreItemSpec();
        var supplierStoreItems = await _supplierStoreItemRepository.ListAsync(getAllSupplierStoreItemSpec, cancellationToken);
        
        return _mapper.Map<IEnumerable<SupplierStoreItemDto>>(supplierStoreItems);
    }

    public async Task<int> ConnectSupplierStoreItemAsync(SupplierStoreItemDto supplierStoreItemDto, CancellationToken cancellationToken)
    {
        var supplier = await _supplierRepository.GetByIdAsync(supplierStoreItemDto.Supplier.Id, cancellationToken);
        if (supplier is null) throw new NotFoundException($"Supplier with id {supplierStoreItemDto.Supplier.Id} does not exist");
        
        var storeItem = await _storeItemRepository.GetByIdAsync(supplierStoreItemDto.StoreItem.Id, cancellationToken);
        if (storeItem is null) throw new NotFoundException($"Store item with id {supplierStoreItemDto.StoreItem.Id} does not exist");

        var getSupplierStoreItemSpec = new GetSupplierStoreItemForConnectionSpec(
            supplierStoreItemDto.Supplier.Id,
            supplierStoreItemDto.StoreItem.Id,
            supplierStoreItemDto.Quarter,
            supplierStoreItemDto.Year
        );
        var supplierStoreItem = await _supplierStoreItemRepository.FirstOrDefaultAsync(getSupplierStoreItemSpec, cancellationToken);
        if (supplierStoreItem is not null)
        {
            throw new ApplicationException($"Supplier {supplierStoreItemDto.Supplier.Id} already has store item with id {supplierStoreItemDto.StoreItem.Id}");
        }

        supplierStoreItem = new SupplierStoreItem(
            supplierStoreItemDto.Supplier.Id,
            supplierStoreItemDto.StoreItem.Id,
            supplierStoreItemDto.StartDate,
            supplierStoreItemDto.EndDate, 
            supplierStoreItemDto.Quarter,
            supplierStoreItemDto.Year,
            supplierStoreItemDto.ItemPrice,
            supplierStoreItemDto.SoldItems
        );
        
        await _supplierStoreItemRepository.AddAsync(supplierStoreItem, cancellationToken);
        await _supplierStoreItemRepository.SaveChangesAsync(cancellationToken);
        return supplierStoreItem.Id;
    }

    public async Task DisconnectSupplierStoreItemAsync(int supplierId, int storeItemId, CancellationToken cancellationToken)
    {
        var getSupplierStoreItemSpec = new GetSupplierStoreItemSpec(supplierId, storeItemId);
        var supplierStoreItem = await _supplierStoreItemRepository.FirstOrDefaultAsync(getSupplierStoreItemSpec, cancellationToken);
        if (supplierStoreItem is null) throw new ApplicationException($"Supplier {supplierId} does not have store item with id {storeItemId}");
        
        await _supplierStoreItemRepository.DeleteAsync(supplierStoreItem, cancellationToken);
        await _supplierStoreItemRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> GetSoldItemsCountAsync(int id, CancellationToken cancellationToken)
    {
        var getSupplierSpec = new GetSupplierWithStoreItemsSpec(id);
        var supplier = await _supplierRepository.FirstOrDefaultAsync(getSupplierSpec, cancellationToken);
        if (supplier is null) throw new NotFoundException($"Supplier with id {id} does not exist");
        
        var soldItemsCount = supplier.SupplierStoreItems.Sum(x => x.SoldItems);
        return soldItemsCount;
    }

    public async Task<SupplierStoreItemDto> GetLowestItemPriceForProductAsync(int productId, CancellationToken cancellationToken)
    {
        var getSupplierStoreItemSpec = new GetSupplierStoreItemsByStoreItemIdSpec(productId);
        var supplierStoreItems = await _supplierStoreItemRepository.ListAsync(getSupplierStoreItemSpec, cancellationToken);
        var supplierStoreItem = supplierStoreItems.FirstOrDefault();
        if (supplierStoreItem is null) throw new ApplicationException($"Product with id {productId} is not available from any supplier");

        return _mapper.Map<SupplierStoreItemDto>(supplierStoreItem);
    }
}