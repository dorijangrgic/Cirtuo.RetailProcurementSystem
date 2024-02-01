using Cirtuo.RetailProcurementSystem.Application.Suppliers.Models;
using Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Models;

namespace Cirtuo.RetailProcurementSystem.Testing.Builders;

public class SupplierSoldItemsResponseBuilder
{
    private SupplierDto _supplierDto;
    private int _soldItemsCount;
    
    public static SupplierSoldItemsResponseBuilder Default()
    {
        return new SupplierSoldItemsResponseBuilder()
            .WithSupplierDto(SupplierDtoBuilder.Default().Build())
            .WithSoldItemsCount(12345);
    }
    
    public SupplierSoldItemsResponseBuilder WithSupplierDto(SupplierDto supplierDto)
    {
        _supplierDto = supplierDto;
        return this;
    }
    
    public SupplierSoldItemsResponseBuilder WithSoldItemsCount(int soldItemsCount)
    {
        _soldItemsCount = soldItemsCount;
        return this;
    }
    
    public SupplierSoldItemsResponse Build() => new(_supplierDto, _soldItemsCount);
}