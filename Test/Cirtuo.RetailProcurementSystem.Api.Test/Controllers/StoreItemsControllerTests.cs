using Cirtuo.RetailProcurementSystem.Api.Controllers;
using Cirtuo.RetailProcurementSystem.Application.StoreItems.Models;
using Cirtuo.RetailProcurementSystem.Application.StoreItems.Services;
using Cirtuo.RetailProcurementSystem.Testing.Builders;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace Cirtuo.RetailProcurementSystem.Api.Test.Controllers;

public class StoreItemsControllerTests
{
    private readonly IStoreItemService _storeItemService;
    private readonly StoreItemsController _controller;
    
    public StoreItemsControllerTests()
    {
        _storeItemService = Substitute.For<IStoreItemService>();
        _controller = new StoreItemsController(_storeItemService);
    }
    
    [Fact]
    public async Task GetStoreItemsAsync_ReturnsOk()
    {
        // Arrange
        var storeItemDtos = new List<StoreItemDto>
        {
            StoreItemDtoBuilder.Default().WithId(1).Build(),
            StoreItemDtoBuilder.Default().WithId(2).Build(),
            StoreItemDtoBuilder.Default().WithId(3).Build(),
        };
        _storeItemService.GetStoreItemsAsync(default).Returns(storeItemDtos);
        
        // Act
        var result = await _controller.GetStoreItems(default);
        
        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        result.Result.As<OkObjectResult>().Value.Should().BeEquivalentTo(storeItemDtos);
    }
    
    [Fact]
    public async Task GetStoreItemAsync_ReturnsOk()
    {
        // Arrange
        const int storeItemId = 1;
        var storeItemDto = StoreItemDtoBuilder.Default().WithId(storeItemId).Build();
        _storeItemService.GetStoreItemAsync(storeItemId, default).Returns(storeItemDto);
        
        // Act
        var result = await _controller.GetStoreItemDetails(storeItemId, default);
        
        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        result.Result.As<OkObjectResult>().Value.Should().BeEquivalentTo(storeItemDto);
    }
    
    
    [Fact]
    public async Task CreateStoreItemAsync_ReturnsCreated()
    {
        // Arrange
        var storeItemDto = StoreItemDtoBuilder.Default().Build();
        const int storeItemId = 1;
        _storeItemService.CreateStoreItemAsync(storeItemDto, default).Returns(storeItemId);
        
        // Act
        var result = await _controller.CreateStoreItem(storeItemDto, default);
        
        // Assert
        result.Should().BeOfType<CreatedResult>();
        result.As<CreatedResult>().Location.Should().Be($"store-items/{storeItemId}");
    }
    
    [Fact]
    public async Task UpdateStoreItemAsync_ReturnsNoContent()
    {
        // Arrange
        const int storeItemId = 1;
        var storeItemDto = StoreItemDtoBuilder.Default().Build();
        
        // Act
        var result = await _controller.UpdateStoreItem(storeItemId, storeItemDto, default);
        
        // Assert
        result.Should().BeOfType<NoContentResult>();
    }
}