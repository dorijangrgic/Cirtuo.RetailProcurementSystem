using Cirtuo.RetailProcurementSystem.Api.Controllers;
using Cirtuo.RetailProcurementSystem.Application.SupplierRetailers.Services;
using Cirtuo.RetailProcurementSystem.Application.Suppliers.Models;
using Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Models;
using Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Services;
using Cirtuo.RetailProcurementSystem.Testing.Builders;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace Cirtuo.RetailProcurementSystem.Api.Test.Controllers;

public class StatisticsControllerTests
{
    private readonly ISupplierStoreItemService _supplierStoreItemService;
    private readonly ISupplierRetailerService _supplierRetailerService;
    private readonly StatisticsController _controller;
    
    public StatisticsControllerTests()
    {
        _supplierStoreItemService = Substitute.For<ISupplierStoreItemService>();
        _supplierRetailerService = Substitute.For<ISupplierRetailerService>();
        _controller = new StatisticsController(_supplierStoreItemService, _supplierRetailerService);
    }
    
    [Fact]
    public async Task GetSupplierSoldItemsCount_ReturnsOk()
    {
        // Arrange
        var id = 1;
        var count = 123;

        var supplierSoldItems = SupplierSoldItemsResponseBuilder.Default().WithSoldItemsCount(count).Build();
        _supplierStoreItemService.GetSoldItemsCountAsync(id, default).Returns(supplierSoldItems);
        
        // Act
        var result = await _controller.GetSupplierSoldItemsCount(id, default);
        
        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        result.Result.As<OkObjectResult>().Value.Should().BeEquivalentTo(supplierSoldItems);
    }
    
    [Fact]
    public async Task GetBestProductOffer_ReturnsOk()
    {
        // Arrange
        var productId = 1;
        var supplierStoreItem = SupplierStoreItemDtoBuilder.Default().Build();
        _supplierStoreItemService.GetLowestItemPriceForProductAsync(productId, default).Returns(supplierStoreItem);
        
        // Act
        var result = await _controller.GetBestProductOffer(productId, default);
        
        // Assert
        result.Should().BeOfType<OkObjectResult>();
        result.As<OkObjectResult>().Value.Should().BeEquivalentTo(supplierStoreItem);
    }
    
    [Fact]
    public async Task PlanSuppliersForUpcomingQuarter_ReturnsNoContent()
    {
        // Arrange
        var connectSupplierRetailerRequest = ConnectSupplierRetailerRequestBuilder.Default().Build();
        
        // Act
        var result = await _controller.PlanSuppliersForUpcomingQuarter(connectSupplierRetailerRequest, default);
        
        // Assert
        result.Should().BeOfType<NoContentResult>();
    }
    
    [Fact]
    public async Task GetSuppliersForCurrentQuarter_ReturnsOk()
    {
        // Arrange
        var suppliers = new List<SupplierDto>
        {
            SupplierDtoBuilder.Default().WithId(1).Build(),
            SupplierDtoBuilder.Default().WithId(2).Build(),
            SupplierDtoBuilder.Default().WithId(3).Build()
        };
        _supplierRetailerService.GetSuppliersForCurrentQuarterAsync(default).Returns(suppliers);
        
        // Act
        var result = await _controller.GetSuppliersForCurrentQuarter(default);
        
        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        result.Result.As<OkObjectResult>().Value.Should().BeEquivalentTo(suppliers);
    }
}