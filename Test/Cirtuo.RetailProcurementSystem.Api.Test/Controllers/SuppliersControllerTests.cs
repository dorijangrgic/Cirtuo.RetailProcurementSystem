using Cirtuo.RetailProcurementSystem.Api.Controllers;
using Cirtuo.RetailProcurementSystem.Application.Suppliers.Models;
using Cirtuo.RetailProcurementSystem.Application.Suppliers.Services;
using Cirtuo.RetailProcurementSystem.Testing.Builders;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace Cirtuo.RetailProcurementSystem.Api.Test.Controllers;

public class SuppliersControllerTests
{
    private readonly ISupplierService _supplierService;
    private readonly SuppliersController _suppliersController;

    public SuppliersControllerTests()
    {
        _supplierService = Substitute.For<ISupplierService>();
        _suppliersController = new SuppliersController(_supplierService);
    }
    
    [Fact]
    public async Task GetSuppliersAsync_ReturnsOk()
    {
        // Arrange
        var supplierDtos = new List<SupplierDto>
        {
            SupplierDtoBuilder.Default().WithId(1).Build(),
            SupplierDtoBuilder.Default().WithId(2).Build(),
            SupplierDtoBuilder.Default().WithId(3).Build(),
        };
        _supplierService.GetSuppliersAsync(default).Returns(supplierDtos);
        
        // Act
        var result = await _suppliersController.GetSuppliers(default);
        
        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        result.Result.As<OkObjectResult>().Value.Should().BeEquivalentTo(supplierDtos);
    }
    
    [Fact]
    public async Task GetSupplierAsync_ReturnsOk()
    {
        // Arrange
        const int supplierId = 1;
        var supplierDto = SupplierDtoBuilder.Default().WithId(supplierId).Build();
        _supplierService.GetSupplierAsync(supplierId, default).Returns(supplierDto);
        
        // Act
        var result = await _suppliersController.GetSupplierDetails(supplierId, default);
        
        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        result.Result.As<OkObjectResult>().Value.Should().BeEquivalentTo(supplierDto);
    }
    
    [Fact]
    public async Task CreateSupplierAsync_ReturnsCreated()
    {
        // Arrange
        var supplierId = 1;
        var supplierDto = SupplierDtoBuilder.Default().Build();
        _supplierService.CreateSupplierAsync(supplierDto, default).Returns(supplierId);
        
        // Act
        var result = await _suppliersController.CreateSupplier(supplierDto, default);
        
        // Assert
        result.Should().BeOfType<CreatedResult>();
        result.As<CreatedResult>().Location.Should().Be($"suppliers/{supplierId}");
    }
    
    [Fact]
    public async Task UpdateSupplierAsync_ReturnsNoContent()
    {
        // Arrange
        const int supplierId = 1;
        var supplierDto = SupplierDtoBuilder.Default().Build();
        
        // Act
        var result = await _suppliersController.UpdateSupplier(supplierId, supplierDto, default);
        
        // Assert
        result.Should().BeOfType<NoContentResult>();
    }
    
    [Fact]
    public async Task DeleteSupplierAsync_ReturnsNoContent()
    {
        // Arrange
        const int supplierId = 1;
        
        // Act
        var result = await _suppliersController.DeleteSupplier(supplierId, default);
        
        // Assert
        result.Should().BeOfType<NoContentResult>();
    }
}