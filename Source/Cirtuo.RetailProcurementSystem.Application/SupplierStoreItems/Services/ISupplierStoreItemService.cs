using Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Models;

namespace Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Services;

public interface ISupplierStoreItemService
{
    Task<IEnumerable<SupplierStoreItemDto>> GetSupplierStoreItemsAsync(CancellationToken cancellationToken);
    Task<int> ConnectSupplierStoreItemAsync(SupplierStoreItemDto supplierStoreItemDto, CancellationToken cancellationToken);
    Task DisconnectSupplierStoreItemAsync(int supplierId, int storeItemId, CancellationToken cancellationToken);
    Task<int> GetSoldItemsCountAsync(int id, CancellationToken cancellationToken);
    Task<SupplierStoreItemDto> GetLowestItemPriceForProductAsync(int productId, CancellationToken cancellationToken);
}