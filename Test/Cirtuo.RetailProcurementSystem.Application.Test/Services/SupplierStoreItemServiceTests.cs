using Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Service;
using Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Services;
using Cirtuo.RetailProcurementSystem.Application.Test.Builders;
using Cirtuo.RetailProcurementSystem.Domain;
using Cirtuo.RetailProcurementSystem.Persistence;
using FluentAssertions;

namespace Cirtuo.RetailProcurementSystem.Application.Test.Services;

public class SupplierStoreItemServiceTests : IntegrationTestFixture
{
    private readonly ISupplierStoreItemService _supplierStoreItemService;
    private readonly IGenericRepository<SupplierStoreItem> _supplierStoreItemRepository;

    public SupplierStoreItemServiceTests()
    {
        var dbContext = new RetailProcurementDbContext(DbContextOptions);
        _supplierStoreItemRepository = new GenericRepository<SupplierStoreItem>(dbContext);
        var supplierRepository = new GenericRepository<Supplier>(dbContext);
        var storeItemRepository = new GenericRepository<StoreItem>(dbContext);
        _supplierStoreItemService = new SupplierStoreItemService(_supplierStoreItemRepository, supplierRepository, storeItemRepository);
    }
    
    [Fact]
    public async Task GetSupplierStoreItemsAsync_ReturnsSupplierStoreItems()
    {
        // Arrange
        // Act
        var supplierStoreItems = await _supplierStoreItemService.GetSupplierStoreItemsAsync();
        
        // Assert
        supplierStoreItems.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task ConnectSupplierStoreItemAsync_ConnectsSupplierStoreItem()
    {
        // Arrange
        var supplierStoreItemDto = SupplierStoreItemDtoBuilder.Default().Build();
        
        // Act
        var supplierStoreItemId = await _supplierStoreItemService.ConnectSupplierStoreItemAsync(supplierStoreItemDto);
        
        // Assert
        var supplierStoreItems = await _supplierStoreItemService.GetSupplierStoreItemsAsync();
        supplierStoreItems.Should().Contain(x => x.Id == supplierStoreItemId);
    }
    
    [Fact]
    public async Task ConnectSupplierStoreItemAsync_SupplierMissing_ThrowsApplicationException()
    {
        // Arrange
        var supplierStoreItemDto = SupplierStoreItemDtoBuilder.Default()
            .WithSupplier(SupplierDtoBuilder.Default().WithId(10_000).Build())
            .Build();
        
        // Act
        Func<Task> act = async () => await _supplierStoreItemService.ConnectSupplierStoreItemAsync(supplierStoreItemDto);
        
        // Assert
        await act.Should().ThrowAsync<ApplicationException>();
    }
    
    [Fact]
    public async Task ConnectSupplierStoreItemAsync_StoreItemMissing_ThrowsApplicationException()
    {
        // Arrange
        var supplierStoreItemDto = SupplierStoreItemDtoBuilder.Default()
            .WithStoreItem(StoreItemDtoBuilder.Default().WithId(10_000).Build())
            .Build();
        
        // Act
        Func<Task> act = async () => await _supplierStoreItemService.ConnectSupplierStoreItemAsync(supplierStoreItemDto);
        
        // Assert
        await act.Should().ThrowAsync<ApplicationException>();
    }

    [Fact]
    public async Task ConnectSupplierStoreItemAsync_SupplierStoreItemAlreadyExists_ThrowsApplicationException()
    {
        // Arrange
        const int supplierId = 31;
        const int storeItemId = 21;
        var supplierStoreItemDto = SupplierStoreItemDtoBuilder
            .Default()
            .WithSupplier(SupplierDtoBuilder.Default().WithId(supplierId).Build())
            .WithStoreItem(StoreItemDtoBuilder.Default().WithId(storeItemId).Build())
            .Build();
        await _supplierStoreItemService.ConnectSupplierStoreItemAsync(supplierStoreItemDto);
        
        // Act
        Func<Task> act = async () => await _supplierStoreItemService.ConnectSupplierStoreItemAsync(supplierStoreItemDto);
        
        // Assert
        await act.Should().ThrowAsync<ApplicationException>();
    }
    
    [Fact]
    public async Task DisconnectSupplierStoreItemAsync_DisconnectsSupplierStoreItem()
    {
        // Arrange
        const int supplierId = 31;
        const int storeItemId = 21;
        var supplierStoreItemDto = SupplierStoreItemDtoBuilder
            .Default()
            .WithSupplier(SupplierDtoBuilder.Default().WithId(supplierId).Build())
            .WithStoreItem(StoreItemDtoBuilder.Default().WithId(storeItemId).Build())
            .Build();
        await _supplierStoreItemService.ConnectSupplierStoreItemAsync(supplierStoreItemDto);
        
        // Act
        await _supplierStoreItemService.DisconnectSupplierStoreItemAsync(supplierId, storeItemId);
        
        // Assert
        var supplierStoreItems = await _supplierStoreItemService.GetSupplierStoreItemsAsync();
        supplierStoreItems.Should().NotContain(x => x.Supplier.Id == supplierId && x.StoreItem.Id == storeItemId);
    }
    
    [Fact]
    public async Task DisconnectSupplierStoreItemAsync_SupplierStoreItemMissing_ThrowsApplicationException()
    {
        // Arrange
        const int supplierId = 31;
        const int storeItemId = 21;
        
        // Act
        Func<Task> act = async () => await _supplierStoreItemService.DisconnectSupplierStoreItemAsync(supplierId, storeItemId);
        
        // Assert
        await act.Should().ThrowAsync<ApplicationException>();
    }
    
    [Fact]
    public async Task GetSoldItemsCountAsync_ReturnsSoldItemsCount()
    {
        // Arrange
        await _supplierStoreItemRepository.DeleteRangeAsync(await _supplierStoreItemRepository.ListAsync());
        const int supplierId = 40;
        var supplierStoreItemDto1 = SupplierStoreItemDtoBuilder
            .Default()
            .WithSoldItems(123)
            .WithSupplier(SupplierDtoBuilder.Default().WithId(supplierId).Build())
            .WithStoreItem(StoreItemDtoBuilder.Default().WithId(21).Build())
            .Build();
        await _supplierStoreItemService.ConnectSupplierStoreItemAsync(supplierStoreItemDto1);
        
        var supplierStoreItemDto2 = SupplierStoreItemDtoBuilder
            .Default()
            .WithSoldItems(32)
            .WithSupplier(SupplierDtoBuilder.Default().WithId(supplierId).Build())
            .WithStoreItem(StoreItemDtoBuilder.Default().WithId(22).Build())
            .Build();
        await _supplierStoreItemService.ConnectSupplierStoreItemAsync(supplierStoreItemDto2);
        
        var supplierStoreItemDto3 = SupplierStoreItemDtoBuilder
            .Default()
            .WithSoldItems(899)
            .WithSupplier(SupplierDtoBuilder.Default().WithId(supplierId).Build())
            .WithStoreItem(StoreItemDtoBuilder.Default().WithId(23).Build())
            .Build();
        await _supplierStoreItemService.ConnectSupplierStoreItemAsync(supplierStoreItemDto3);
        
        // Act
        var soldItemsCount = await _supplierStoreItemService.GetSoldItemsCountAsync(supplierId);
        
        // Assert
        soldItemsCount.Should().Be(supplierStoreItemDto1.SoldItems + supplierStoreItemDto2.SoldItems + supplierStoreItemDto3.SoldItems);
    }
    
    [Fact]
    public async Task GetLowestItemPriceForProductAsync_ReturnsLowestItemPriceForProduct()
    {
        // Arrange
        await _supplierStoreItemRepository.DeleteRangeAsync(await _supplierStoreItemRepository.ListAsync());
        const int productId = 13;
        var supplierStoreItemDto1 = SupplierStoreItemDtoBuilder
            .Default()
            .WithItemPrice(10)
            .WithSupplier(SupplierDtoBuilder.Default().WithId(10).Build())
            .WithStoreItem(StoreItemDtoBuilder.Default().WithId(productId).Build())
            .Build();
        await _supplierStoreItemService.ConnectSupplierStoreItemAsync(supplierStoreItemDto1);
        
        var supplierStoreItemDto2 = SupplierStoreItemDtoBuilder
            .Default()
            .WithItemPrice(5)
            .WithSupplier(SupplierDtoBuilder.Default().WithId(11).Build())
            .WithStoreItem(StoreItemDtoBuilder.Default().WithId(productId).Build())
            .Build();
        await _supplierStoreItemService.ConnectSupplierStoreItemAsync(supplierStoreItemDto2);
        
        var supplierStoreItemDto3 = SupplierStoreItemDtoBuilder
            .Default()
            .WithItemPrice(15)
            .WithSupplier(SupplierDtoBuilder.Default().WithId(12).Build())
            .WithStoreItem(StoreItemDtoBuilder.Default().WithId(productId).Build())
            .Build();
        await _supplierStoreItemService.ConnectSupplierStoreItemAsync(supplierStoreItemDto3);
        
        // Act
        var lowestItemPriceForProduct = await _supplierStoreItemService.GetLowestItemPriceForProductAsync(productId);
        
        // Assert
        lowestItemPriceForProduct.Should().BeEquivalentTo(supplierStoreItemDto2, options =>
        {
            return options.Excluding(x => x.Id)
                .Excluding(x => x.Supplier)
                .Excluding(x => x.StoreItem);
        });
    }
}