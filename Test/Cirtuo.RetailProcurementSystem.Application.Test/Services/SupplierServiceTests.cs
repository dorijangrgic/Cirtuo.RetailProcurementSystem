using Cirtuo.RetailProcurementSystem.Application.Suppliers.Services;
using Cirtuo.RetailProcurementSystem.Application.Test.Builders;
using Cirtuo.RetailProcurementSystem.Domain;
using Cirtuo.RetailProcurementSystem.Persistence;
using FluentAssertions;

namespace Cirtuo.RetailProcurementSystem.Application.Test.Services;

public class SupplierServiceTests : IClassFixture<IntegrationTestFixture>
{
    private readonly ISupplierService _supplierService;
    
    public SupplierServiceTests(IntegrationTestFixture fixture)
    {
        var dbContext = new RetailProcurementDbContext(fixture.DbContextOptions);
        var supplierRepository = new GenericRepository<Supplier>(dbContext);
        _supplierService = new SupplierService(supplierRepository);
    }
    
    [Fact]
    public async Task GetSuppliersAsync_ReturnsSuppliers()
    {
        // Arrange
        // Act
        var suppliers = await _supplierService.GetSuppliersAsync();
        
        // Assert
        suppliers.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task GetSupplierAsync_ReturnsSupplier()
    {
        // Arrange
        const int id = 1;
        
        // Act
        var supplier = await _supplierService.GetSupplierAsync(id);
        
        // Assert
        supplier.Should().NotBeNull();
    }
    
    [Fact]
    public async Task GetSupplierAsync_SupplierMissing_ThrowsApplicationException()
    {
        // Arrange
        const int id = 0;
        
        // Act
        Func<Task> act = async () => await _supplierService.GetSupplierAsync(id);
        
        // Assert
        await act.Should().ThrowAsync<ApplicationException>();
    }

    [Fact]
    public async Task CreateSupplierAsync_CreatesSupplier()
    {
        // Arrange
        var supplierDto = SupplierDtoBuilder.Default().Build();
        
        // Act
        var id = await _supplierService.CreateSupplierAsync(supplierDto);
        
        // Assert
        var supplier = await _supplierService.GetSupplierAsync(id);
        supplier.Should().NotBeNull();
        supplier.Should().BeEquivalentTo(supplierDto, options =>
        {
            return options.Excluding(x => x.Id).Excluding(x => x.Location).Excluding(x => x.Contact);
        });
    }
    
    [Fact]
    public async Task UpdateSupplierAsync_UpdatesSupplier()
    {
        // Arrange
        const int id = 1;
        var supplierDto = SupplierDtoBuilder.Default().WithName("Updated supplier").Build();
        
        // Act
        await _supplierService.UpdateSupplierAsync(id, supplierDto);
        
        // Assert
        var supplier = await _supplierService.GetSupplierAsync(id);
        supplier.Should().NotBeNull();
        supplier.Should().BeEquivalentTo(supplierDto, options =>
        {
            return options.Excluding(x => x.Id).Excluding(x => x.Location).Excluding(x => x.Contact);
        });
    }
    
    [Fact]
    public async Task UpdateSupplierAsync_SupplierMissing_ThrowsApplicationException()
    {
        // Arrange
        const int id = 0;
        var supplierDto = SupplierDtoBuilder.Default().WithName("Updated supplier").Build();
        
        // Act
        Func<Task> act = async () => await _supplierService.UpdateSupplierAsync(id, supplierDto);
        
        // Assert
        await act.Should().ThrowAsync<ApplicationException>();
    }
    
    [Fact]
    public async Task DeleteSupplierAsync_DeletesSupplier()
    {
        // Arrange
        const int id = 2;
        
        // Act
        await _supplierService.DeleteSupplierAsync(id);
        
        // Assert
        Func<Task> act = async () => await _supplierService.GetSupplierAsync(id);
        await act.Should().ThrowAsync<ApplicationException>();
    }
    
    [Fact]
    public async Task DeleteSupplierAsync_SupplierMissing_ThrowsApplicationException()
    {
        // Arrange
        const int id = 10_000;
        
        // Act
        Func<Task> act = async () => await _supplierService.DeleteSupplierAsync(id);
        
        // Assert
        await act.Should().ThrowAsync<ApplicationException>();
    }
}