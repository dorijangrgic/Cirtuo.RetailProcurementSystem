using Cirtuo.RetailProcurementSystem.Application.StoreItems.Models;
using Cirtuo.RetailProcurementSystem.Application.Suppliers.Models;
using Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Models;

namespace Cirtuo.RetailProcurementSystem.Application.Test.Builders;

public class SupplierStoreItemDtoBuilder
{
    private int _id;
    private DateTime _startDate;
    private DateTime _endDate;
    private int _quarter;
    private int _year;
    private decimal _itemPrice;
    private int _soldItems;
    private SupplierDto _supplier;
    private StoreItemDto _storeItem;
    
    public static SupplierStoreItemDtoBuilder Default()
    {
        var startDate = new DateTime(2023, 1, 1).ToUniversalTime();
        var endDate = startDate.AddMonths(3);
        var quarter = 1;
        var year = 2023;
        
        return new SupplierStoreItemDtoBuilder()
            .WithId(1)
            .WithStartDate(startDate)
            .WithEndDate(endDate)
            .WithQuarter(quarter)
            .WithYear(year)
            .WithItemPrice(1.99m)
            .WithSoldItems(1)
            .WithSupplier(SupplierDtoBuilder.Default().WithId(1).Build())
            .WithStoreItem(StoreItemDtoBuilder.Default().WithId(1).Build());
    }
    
    public SupplierStoreItemDtoBuilder WithId(int id)
    {
        _id = id;
        return this;
    }
    
    public SupplierStoreItemDtoBuilder WithStartDate(DateTime startDate)
    {
        _startDate = startDate;
        return this;
    }
    
    public SupplierStoreItemDtoBuilder WithEndDate(DateTime endDate)
    {
        _endDate = endDate;
        return this;
    }
    
    public SupplierStoreItemDtoBuilder WithQuarter(int quarter)
    {
        _quarter = quarter;
        return this;
    }
    
    public SupplierStoreItemDtoBuilder WithYear(int year)
    {
        _year = year;
        return this;
    }
    
    public SupplierStoreItemDtoBuilder WithItemPrice(decimal itemPrice)
    {
        _itemPrice = itemPrice;
        return this;
    }
    
    public SupplierStoreItemDtoBuilder WithSoldItems(int soldItems)
    {
        _soldItems = soldItems;
        return this;
    }   
    
    public SupplierStoreItemDtoBuilder WithSupplier(SupplierDto supplier)
    {
        _supplier = supplier;
        return this;
    }
    
    public SupplierStoreItemDtoBuilder WithStoreItem(StoreItemDto storeItem)
    {
        _storeItem = storeItem;
        return this;
    }
    
    public SupplierStoreItemDto Build()
    {
        return new SupplierStoreItemDto(
            _id,
            _startDate,
            _endDate,
            _quarter,
            _year,
            _itemPrice,
            _soldItems,
            _supplier,
            _storeItem
        );
    }
}