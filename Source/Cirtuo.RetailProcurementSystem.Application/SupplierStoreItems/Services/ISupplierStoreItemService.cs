using Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Models;

namespace Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Service;

public interface ISupplierStoreItemService
{
    Task<IEnumerable<SupplierStoreItemDto>> GetSupplierStoreItemsAsync();
    Task<int> ConnectSupplierStoreItemAsync(SupplierStoreItemDto supplierStoreItemDto);
    Task DisconnectSupplierStoreItemAsync(int supplierId, int storeItemId);
    Task<int> GetSoldItemsCountAsync(int id);
    Task<SupplierStoreItemDto> GetLowestItemPriceForProductAsync(int productId);
}