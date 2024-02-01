using Cirtuo.RetailProcurementSystem.Application.Common;
using Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Models;
using Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Services;
using Cirtuo.RetailProcurementSystem.Domain;
using Cirtuo.RetailProcurementSystem.Persistence;
using Cirtuo.RetailProcurementSystem.Testing.Builders;
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
        
        _supplierStoreItemService = new SupplierStoreItemService(
            _supplierStoreItemRepository,
            supplierRepository,
            storeItemRepository,
            Mapper
        );
    }
    
    [Fact]
    public async Task GetSupplierStoreItemsAsync_ReturnsSupplierStoreItems()
    {
        // Arrange
        // Act
        var supplierStoreItems = await _supplierStoreItemService.GetSupplierStoreItemsAsync(default);
        
        // Assert
        supplierStoreItems.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task ConnectSupplierStoreItemAsync_ConnectsSupplierStoreItem()
    {
        // Arrange
        var supplierStoreItemDto = SupplierStoreItemDtoBuilder.Default().Build();
        
        // Act
        var supplierStoreItemId = await _supplierStoreItemService.ConnectSupplierStoreItemAsync(supplierStoreItemDto, default);
        
        // Assert
        var supplierStoreItems = await _supplierStoreItemService.GetSupplierStoreItemsAsync(default);
        supplierStoreItems.Should().Contain(x => x.Id == supplierStoreItemId);
    }
    
    [Fact]
    public async Task ConnectSupplierStoreItemAsync_SupplierMissing_ThrowsNotFoundException()
    {
        // Arrange
        var supplierStoreItemDto = SupplierStoreItemDtoBuilder.Default()
            .WithSupplier(SupplierDtoBuilder.Default().WithId(10_000).Build())
            .Build();
        
        // Act
        Func<Task> act = async () => await _supplierStoreItemService.ConnectSupplierStoreItemAsync(supplierStoreItemDto, default);
        
        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
    
    [Fact]
    public async Task ConnectSupplierStoreItemAsync_StoreItemMissing_ThrowsNotFoundException()
    {
        // Arrange
        var supplierStoreItemDto = SupplierStoreItemDtoBuilder.Default()
            .WithStoreItem(StoreItemDtoBuilder.Default().WithId(10_000).Build())
            .Build();
        
        // Act
        Func<Task> act = async () => await _supplierStoreItemService.ConnectSupplierStoreItemAsync(supplierStoreItemDto, default);
        
        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
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
        await _supplierStoreItemService.ConnectSupplierStoreItemAsync(supplierStoreItemDto, default);
        
        // Act
        Func<Task> act = async () => await _supplierStoreItemService.ConnectSupplierStoreItemAsync(supplierStoreItemDto, default);
        
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
        await _supplierStoreItemService.ConnectSupplierStoreItemAsync(supplierStoreItemDto, default);
        
        // Act
        await _supplierStoreItemService.DisconnectSupplierStoreItemAsync(supplierId, storeItemId, default);
        
        // Assert
        var supplierStoreItems = await _supplierStoreItemService.GetSupplierStoreItemsAsync(default);
        supplierStoreItems.Should().NotContain(x => x.Supplier.Id == supplierId && x.StoreItem.Id == storeItemId);
    }
    
    [Fact]
    public async Task DisconnectSupplierStoreItemAsync_SupplierStoreItemMissing_ThrowsApplicationException()
    {
        // Arrange
        const int supplierId = 31;
        const int storeItemId = 21;
        
        // Act
        Func<Task> act = async () => await _supplierStoreItemService.DisconnectSupplierStoreItemAsync(supplierId, storeItemId, default);
        
        // Assert
        await act.Should().ThrowAsync<ApplicationException>();
    }
    
    [Fact]
    public async Task GetSoldItemsCountAsync_ReturnsSoldItemsCount()
    {
        // Arrange
        await _supplierStoreItemRepository.DeleteRangeAsync(await _supplierStoreItemRepository.ListAsync());
        const int supplierId = 40;
        var supplier = SupplierDtoBuilder.Default().WithId(supplierId).Build();
        var supplierStoreItemDto1 = SupplierStoreItemDtoBuilder
            .Default()
            .WithSoldItems(123)
            .WithSupplier(supplier)
            .WithStoreItem(StoreItemDtoBuilder.Default().WithId(21).Build())
            .Build();
        await _supplierStoreItemService.ConnectSupplierStoreItemAsync(supplierStoreItemDto1, default);
        
        var supplierStoreItemDto2 = SupplierStoreItemDtoBuilder
            .Default()
            .WithSoldItems(32)
            .WithSupplier(supplier)
            .WithStoreItem(StoreItemDtoBuilder.Default().WithId(22).Build())
            .Build();
        await _supplierStoreItemService.ConnectSupplierStoreItemAsync(supplierStoreItemDto2, default);
        
        var supplierStoreItemDto3 = SupplierStoreItemDtoBuilder
            .Default()
            .WithSoldItems(899)
            .WithSupplier(supplier)
            .WithStoreItem(StoreItemDtoBuilder.Default().WithId(23).Build())
            .Build();
        await _supplierStoreItemService.ConnectSupplierStoreItemAsync(supplierStoreItemDto3, default);

        var supplierSoldItems = SupplierSoldItemsResponseBuilder.Default()
            .WithSupplierDto(supplier)
            .WithSoldItemsCount(supplierStoreItemDto1.SoldItems + supplierStoreItemDto2.SoldItems + supplierStoreItemDto3.SoldItems)
            .Build();
        
        // Act
        var soldItems = await _supplierStoreItemService.GetSoldItemsCountAsync(supplierId, default);
        
        // Assert
        soldItems.ItemsCount.Should().Be(supplierSoldItems.ItemsCount);
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
        await _supplierStoreItemService.ConnectSupplierStoreItemAsync(supplierStoreItemDto1, default);
        
        var supplierStoreItemDto2 = SupplierStoreItemDtoBuilder
            .Default()
            .WithItemPrice(5)
            .WithSupplier(SupplierDtoBuilder.Default().WithId(11).Build())
            .WithStoreItem(StoreItemDtoBuilder.Default().WithId(productId).Build())
            .Build();
        await _supplierStoreItemService.ConnectSupplierStoreItemAsync(supplierStoreItemDto2, default);
        
        var supplierStoreItemDto3 = SupplierStoreItemDtoBuilder
            .Default()
            .WithItemPrice(15)
            .WithSupplier(SupplierDtoBuilder.Default().WithId(12).Build())
            .WithStoreItem(StoreItemDtoBuilder.Default().WithId(productId).Build())
            .Build();
        await _supplierStoreItemService.ConnectSupplierStoreItemAsync(supplierStoreItemDto3, default);
        
        // Act
        var lowestItemPriceForProduct = await _supplierStoreItemService.GetLowestItemPriceForProductAsync(productId, default);
        
        // Assert
        lowestItemPriceForProduct.Should().BeEquivalentTo(supplierStoreItemDto2, options =>
        {
            return options.Excluding(x => x.Id)
                .Excluding(x => x.Supplier)
                .Excluding(x => x.StoreItem);
        });
    }
}