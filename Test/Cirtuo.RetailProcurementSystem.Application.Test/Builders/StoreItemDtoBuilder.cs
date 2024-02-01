using Cirtuo.RetailProcurementSystem.Application.StoreItems.Models;
using Cirtuo.RetailProcurementSystem.Application.StoreItems.Specifications;

namespace Cirtuo.RetailProcurementSystem.Application.Test.Builders;

public class StoreItemDtoBuilder
{
    private int _id;
    private string _name;
    private string _description;
    private string _sku;
    private StoreItemCategoryDto _category;

    public static StoreItemDtoBuilder Default()
    {
        return new StoreItemDtoBuilder()
            .WithId(1)
            .WithName("Test Store Item")
            .WithDescription("Test Store Item Description")
            .WithSku("SKU")
            .WithCategory(StoreItemCategoryDto.Appliances);
    }

    public StoreItemDtoBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public StoreItemDtoBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public StoreItemDtoBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public StoreItemDtoBuilder WithSku(string sku)
    {
        _sku = sku;
        return this;
    }

    public StoreItemDtoBuilder WithCategory(StoreItemCategoryDto category)
    {
        _category = category;
        return this;
    }

    public StoreItemDto Build() => new(_id, _name, _description, _sku, _category);
}