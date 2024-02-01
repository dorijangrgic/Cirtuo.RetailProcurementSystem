using Cirtuo.RetailProcurementSystem.Api.Controllers;
using Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Models;
using Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Services;
using Cirtuo.RetailProcurementSystem.Testing.Builders;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace Cirtuo.RetailProcurementSystem.Api.Test.Controllers;

public class SupplierStoreItemsControllerTests
{
    private readonly ISupplierStoreItemService _supplierStoreItemService;
    private readonly SupplierStoreItemsController _controller;
    
    public SupplierStoreItemsControllerTests()
    {
        _supplierStoreItemService = Substitute.For<ISupplierStoreItemService>();
        _controller = new SupplierStoreItemsController(_supplierStoreItemService);
    }
    
    [Fact]
    public async Task GetAllSupplierStoreItems_ReturnsOk()
    {
        // Arrange
        var supplierStoreItems = new List<SupplierStoreItemDto>()
        {
            SupplierStoreItemDtoBuilder.Default().WithId(1).Build(),
            SupplierStoreItemDtoBuilder.Default().WithId(2).Build(),
            SupplierStoreItemDtoBuilder.Default().WithId(3).Build(),
            SupplierStoreItemDtoBuilder.Default().WithId(4).Build()
        };
        _supplierStoreItemService.GetSupplierStoreItemsAsync(default).Returns(supplierStoreItems);
        
        // Act
        var result = await _controller.GetAllSupplierStoreItems(CancellationToken.None);
        
        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        result.Result.As<OkObjectResult>().Value.Should().BeEquivalentTo(supplierStoreItems);
    }
    
    [Fact]
    public async Task ConnectSupplierStoreItem_ReturnsCreated()
    {
        // Arrange
        var supplierStoreItemId = 1;
        var supplierStoreItemDto = SupplierStoreItemDtoBuilder.Default().WithId(supplierStoreItemId).Build();
        _supplierStoreItemService.ConnectSupplierStoreItemAsync(supplierStoreItemDto, default).Returns(1);
        
        // Act
        var result = await _controller.ConnectSupplierStoreItem(supplierStoreItemDto, CancellationToken.None);
        
        // Assert
        result.Should().BeOfType<CreatedResult>();
        result.As<CreatedResult>().Location.Should().Be($"supplier-store-items/{supplierStoreItemId}");
    }
    
    [Fact]
    public async Task DisconnectSupplierStoreItem_ReturnsOk()
    {
        // Arrange
        var supplierId = 1;
        var storeItemId = 1;
        
        // Act
        var result = await _controller.DisconnectSupplierStoreItem(supplierId, storeItemId, CancellationToken.None);
        
        // Assert
        result.Should().BeOfType<OkResult>();
    }
}