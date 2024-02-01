using Cirtuo.RetailProcurementSystem.Application.Common;
using Cirtuo.RetailProcurementSystem.Application.Common.Models;
using Cirtuo.RetailProcurementSystem.Application.StoreItems.Models;
using Cirtuo.RetailProcurementSystem.Application.StoreItems.Specifications;
using Cirtuo.RetailProcurementSystem.Application.Suppliers.Models;
using Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Models;
using Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Service;
using Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Specifications;
using Cirtuo.RetailProcurementSystem.Domain;

namespace Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Services;

public class SupplierStoreItemService : ISupplierStoreItemService
{
    private readonly IGenericRepository<SupplierStoreItem> _supplierStoreItemRepository;
    private readonly IGenericRepository<Supplier> _supplierRepository;
    private readonly IGenericRepository<StoreItem> _storeItemRepository;

    public SupplierStoreItemService(
        IGenericRepository<SupplierStoreItem> supplierStoreItemRepository,
        IGenericRepository<Supplier> supplierRepository,
        IGenericRepository<StoreItem> storeItemRepository
    )
    {
        _supplierStoreItemRepository = supplierStoreItemRepository;
        _supplierRepository = supplierRepository;
        _storeItemRepository = storeItemRepository;
    }

    public async Task<IEnumerable<SupplierStoreItemDto>> GetSupplierStoreItemsAsync()
    {
        var getAllSupplierStoreItemSpec = new GetAllSupplierStoreItemSpec();
        var supplierStoreItems = await _supplierStoreItemRepository.ListAsync(getAllSupplierStoreItemSpec);
        return supplierStoreItems.Select(x =>
        {
            var locationDto = new LocationDto(x.Supplier.Location.Id, x.Supplier.Location.Address, x.Supplier.Location.City, x.Supplier.Location.State, x.Supplier.Location.ZipCode);
            var contactDto = new ContactDto(x.Supplier.Contact.Id, x.Supplier.Contact.Email, x.Supplier.Contact.Phone);
            var supplierDto = new SupplierDto(x.Supplier.Id, x.Supplier.Name, locationDto, contactDto);
            var storeItemDto = new StoreItemDto(x.StoreItem.Id, x.StoreItem.Sku, x.StoreItem.Name, x.StoreItem.Description, (StoreItemCategoryDto)x.StoreItem.Category);
            return new SupplierStoreItemDto(x.Id, x.StartDate, x.EndDate, x.Quarter, x.Year, x.ItemPrice, x.SoldItems, supplierDto, storeItemDto);
        });
    }

    public async Task<int> ConnectSupplierStoreItemAsync(SupplierStoreItemDto supplierStoreItemDto)
    {
        var supplier = await _supplierRepository.GetByIdAsync(supplierStoreItemDto.Supplier.Id);
        if (supplier is null) throw new NotFoundException($"Supplier with id {supplierStoreItemDto.Supplier.Id} does not exist");
        
        var storeItem = await _storeItemRepository.GetByIdAsync(supplierStoreItemDto.StoreItem.Id);
        if (storeItem is null) throw new NotFoundException($"Store item with id {supplierStoreItemDto.StoreItem.Id} does not exist");

        var getSupplierStoreItemSpec = new GetSupplierStoreItemForConnectionSpec(
            supplierStoreItemDto.Supplier.Id,
            supplierStoreItemDto.StoreItem.Id,
            supplierStoreItemDto.Quarter,
            supplierStoreItemDto.Year
        );
        var supplierStoreItem = await _supplierStoreItemRepository.FirstOrDefaultAsync(getSupplierStoreItemSpec);
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
        
        await _supplierStoreItemRepository.AddAsync(supplierStoreItem);
        await _supplierStoreItemRepository.SaveChangesAsync();
        return supplierStoreItem.Id;
    }

    public async Task DisconnectSupplierStoreItemAsync(int supplierId, int storeItemId)
    {
        var getSupplierStoreItemSpec = new GetSupplierStoreItemSpec(supplierId, storeItemId);
        var supplierStoreItem = await _supplierStoreItemRepository.FirstOrDefaultAsync(getSupplierStoreItemSpec);
        if (supplierStoreItem is null) throw new ApplicationException($"Supplier {supplierId} does not have store item with id {storeItemId}");
        
        await _supplierStoreItemRepository.DeleteAsync(supplierStoreItem);
        await _supplierStoreItemRepository.SaveChangesAsync();
    }

    public async Task<int> GetSoldItemsCountAsync(int id)
    {
        var getSupplierSpec = new GetSupplierWithStoreItemsSpec(id);
        var supplier = await _supplierRepository.FirstOrDefaultAsync(getSupplierSpec);
        if (supplier is null) throw new NotFoundException($"Supplier with id {id} does not exist");
        
        var soldItemsCount = supplier.SupplierStoreItems.Sum(x => x.SoldItems);
        return soldItemsCount;
    }

    public async Task<SupplierStoreItemDto> GetLowestItemPriceForProductAsync(int productId)
    {
        var getSupplierStoreItemSpec = new GetSupplierStoreItemsByStoreItemIdSpec(productId);
        var supplierStoreItems = await _supplierStoreItemRepository.ListAsync(getSupplierStoreItemSpec);
        var supplierStoreItem = supplierStoreItems.FirstOrDefault();
        if (supplierStoreItem is null) throw new ApplicationException($"Product with id {productId} is not available from any supplier");

        var locationDto = new LocationDto(supplierStoreItem.Supplier.Location.Id, supplierStoreItem.Supplier.Location.Address, supplierStoreItem.Supplier.Location.City, supplierStoreItem.Supplier.Location.State, supplierStoreItem.Supplier.Location.ZipCode);
        var contactDto = new ContactDto(supplierStoreItem.Supplier.Contact.Id, supplierStoreItem.Supplier.Contact.Email, supplierStoreItem.Supplier.Contact.Phone);
        var supplierDto = new SupplierDto(supplierStoreItem.Supplier.Id, supplierStoreItem.Supplier.Name, locationDto, contactDto);
        var storeItemDto = new StoreItemDto(supplierStoreItem.StoreItem.Id, supplierStoreItem.StoreItem.Sku, supplierStoreItem.StoreItem.Name, supplierStoreItem.StoreItem.Description, (StoreItemCategoryDto)supplierStoreItem.StoreItem.Category);
        return new SupplierStoreItemDto(supplierStoreItem.Id, supplierStoreItem.StartDate, supplierStoreItem.EndDate, supplierStoreItem.Quarter, supplierStoreItem.Year, supplierStoreItem.ItemPrice, supplierStoreItem.SoldItems, supplierDto, storeItemDto);
    }
}