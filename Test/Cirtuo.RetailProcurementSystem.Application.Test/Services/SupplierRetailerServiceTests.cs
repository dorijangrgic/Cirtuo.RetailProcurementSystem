using Cirtuo.RetailProcurementSystem.Application.Common;
using Cirtuo.RetailProcurementSystem.Application.Common.Services;
using Cirtuo.RetailProcurementSystem.Application.SupplierRetailers.Services;
using Cirtuo.RetailProcurementSystem.Application.Test.Builders;
using Cirtuo.RetailProcurementSystem.Domain;
using Cirtuo.RetailProcurementSystem.Persistence;
using FluentAssertions;

namespace Cirtuo.RetailProcurementSystem.Application.Test.Services;

public class SupplierRetailerServiceTests : IntegrationTestFixture
{
    private readonly ISupplierRetailerService _supplierRetailerService;

    public SupplierRetailerServiceTests()
    {
        var dbContext = new RetailProcurementDbContext(DbContextOptions);
        var supplierRetailerRepository = new GenericRepository<SupplierRetailer>(dbContext);
        var supplierRepository = new GenericRepository<Supplier>(dbContext);
        var retailerRepository = new GenericRepository<Retailer>(dbContext);
        _supplierRetailerService = new SupplierRetailerService(retailerRepository, supplierRepository, supplierRetailerRepository, new DateTimeService());
    }
    
    [Fact]
    public async Task AddSuppliersForUpcomingQuarterAsync_RetailerMissing_ThrowsNotFoundException()
    {
        // Arrange
        var connectSupplierRetailerRequest = ConnectSupplierRetailerRequestBuilder.Default()
            .WithRetailerId(10_000)
            .Build();
        
        // Act
        Func<Task> action = async () => await _supplierRetailerService.AddSuppliersForUpcomingQuarterAsync(connectSupplierRetailerRequest, default);
        
        // Assert
        await action.Should().ThrowAsync<NotFoundException>();
    }
    
    [Fact]
    public async Task AddSuppliersForUpcomingQuarterAsync_SupplierMissing_ThrowsNotFoundException()
    {
        // Arrange
        var supplierIds = new List<int> { 10_000 };
        var connectSupplierRetailerRequest = ConnectSupplierRetailerRequestBuilder.Default()
            .WithSupplierIds(supplierIds)
            .Build();
        
        // Act
        Func<Task> action = async () => await _supplierRetailerService.AddSuppliersForUpcomingQuarterAsync(connectSupplierRetailerRequest, default);
        
        // Assert
        await action.Should().ThrowAsync<NotFoundException>();
    }
    
    [Fact]
    public async Task AddSuppliersForUpcomingQuarterAsync_SupplierAlreadyConnected_ThrowsApplicationException()
    {
        // Arrange
        var supplierIds = new List<int> { 1 };
        var retailerId = 1;
        var connectSupplierRetailerRequest = ConnectSupplierRetailerRequestBuilder.Default()
            .WithRetailerId(retailerId)
            .WithSupplierIds(supplierIds)
            .Build();
        await _supplierRetailerService.AddSuppliersForUpcomingQuarterAsync(connectSupplierRetailerRequest, default);
        
        // Act
        Func<Task> action = async () => await _supplierRetailerService.AddSuppliersForUpcomingQuarterAsync(connectSupplierRetailerRequest, default);
        
        // Assert
        await action.Should().ThrowAsync<ApplicationException>();
    }
    
    [Fact]
    public async Task AddSuppliersForUpcomingQuarterAsync_ShouldConnectSupplierToRetailer()
    {
        // Arrange
        var supplierIds = new List<int> { 1 };
        var retailerId = 1;
        var connectSupplierRetailerRequest = ConnectSupplierRetailerRequestBuilder.Default()
            .WithRetailerId(retailerId)
            .WithSupplierIds(supplierIds)
            .Build();

        // Act
        Func<Task> action = async () => await _supplierRetailerService.AddSuppliersForUpcomingQuarterAsync(connectSupplierRetailerRequest, default);
        
        // Assert
        await action.Should().NotThrowAsync<ApplicationException>();
    }
    
    [Fact]
    public async Task GetSuppliersForCurrentQuarterAsync_ShouldReturnSuppliersForCurrentQuarter()
    {
        // Arrange
        // Act
        var suppliers = await _supplierRetailerService.GetSuppliersForCurrentQuarterAsync(default);
        
        // Assert
        suppliers.Should().NotBeEmpty();
    }
}