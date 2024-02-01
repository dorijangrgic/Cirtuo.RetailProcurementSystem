using Cirtuo.RetailProcurementSystem.Application.StoreItems.Models;
using Cirtuo.RetailProcurementSystem.Application.StoreItems.Services;
using Cirtuo.RetailProcurementSystem.Application.Test.Builders;
using Cirtuo.RetailProcurementSystem.Domain;
using Cirtuo.RetailProcurementSystem.Persistence;
using FluentAssertions;

namespace Cirtuo.RetailProcurementSystem.Application.Test.Services;

public class StoreItemServiceTests : IClassFixture<IntegrationTestFixture>
{
    private readonly IStoreItemService _storeItemService;

    public StoreItemServiceTests(IntegrationTestFixture fixture)
    {
        var dbContext = new RetailProcurementDbContext(fixture.DbContextOptions);
        
        var storeItemRepository = new GenericRepository<StoreItem>(dbContext);
        _storeItemService = new StoreItemService(storeItemRepository);
    }
    
    [Fact]
    public async Task GetStoreItemsAsync_ReturnsStoreItems()
    {
        // Arrange
        // Act
        var storeItems = await _storeItemService.GetStoreItemsAsync();
        
        // Assert
        storeItems.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task GetStoreItemAsync_ReturnsStoreItem()
    {
        // Arrange
        const int storeItemId = 1;
        
        // Act
        var storeItem = await _storeItemService.GetStoreItemAsync(storeItemId);
        
        // Assert
        storeItem.Should().NotBeNull();
    }
    
    [Fact]
    public async Task GetStoreItemAsync_StoreItemMissing_ThrowsApplicationException()
    {
        // Arrange
        const int storeItemId = 10_000;
        
        // Act
        Func<Task<StoreItemDto>> action = async () => await _storeItemService.GetStoreItemAsync(storeItemId);
        
        // Assert
        await action.Should().ThrowAsync<ApplicationException>();
    }
    
    [Fact]
    public async Task CreateStoreItemAsync_CreatesStoreItem()
    {
        // Arrange
        var storeItemDto = StoreItemDtoBuilder.Default().Build();
        
        // Act
        var storeItemId = await _storeItemService.CreateStoreItemAsync(storeItemDto);
        
        // Assert
        var storeItem = await _storeItemService.GetStoreItemAsync(storeItemId);
        storeItem.Should().NotBeNull();
        storeItem.Should().BeEquivalentTo(storeItemDto, options => options.Excluding(x => x.Id));
    }
    
    [Fact]
    public async Task UpdateStoreItemAsync_UpdatesStoreItem()
    {
        // Arrange
        const int storeItemId = 1;
        var updatedStoreItemDto = StoreItemDtoBuilder.Default().WithId(storeItemId).WithName("Updated Store Item").Build();
        
        // Act
        await _storeItemService.UpdateStoreItemAsync(storeItemId, updatedStoreItemDto);
        
        // Assert
        var storeItem = await _storeItemService.GetStoreItemAsync(storeItemId);
        storeItem.Should().NotBeNull();
        storeItem.Should().BeEquivalentTo(updatedStoreItemDto, options => options.Excluding(x => x.Id));
    }
    
    [Fact]
    public async Task UpdateStoreItemAsync_StoreItemMissing_ThrowsApplicationException()
    {
        // Arrange
        const int storeItemId = 1;
        var updatedStoreItemDto = StoreItemDtoBuilder.Default().WithId(storeItemId).WithName("Updated Store Item").Build();
        
        // Act
        Func<Task> action = async () => await _storeItemService.UpdateStoreItemAsync(10_000, updatedStoreItemDto);
        
        // Assert
        await action.Should().ThrowAsync<ApplicationException>();
    }
    
    [Fact]
    public async Task DeleteStoreItemAsync_DeletesStoreItem()
    {
        // Arrange
        const int storeItemId = 2;
        
        // Act
        await _storeItemService.DeleteStoreItemAsync(storeItemId);
        
        // Assert
        Func<Task<StoreItemDto>> action = async () => await _storeItemService.GetStoreItemAsync(storeItemId);
        await action.Should().ThrowAsync<ApplicationException>();
    }
    
    [Fact]
    public async Task DeleteStoreItemAsync_StoreItemMissing_ThrowsApplicationException()
    {
        // Arrange
        const int storeItemId = 10_000;
        
        // Act
        Func<Task> action = async () => await _storeItemService.DeleteStoreItemAsync(storeItemId);
        
        // Assert
        await action.Should().ThrowAsync<ApplicationException>();
    }
}