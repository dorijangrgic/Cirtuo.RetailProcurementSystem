using Cirtuo.RetailProcurementSystem.Application.StoreItems.Specifications;

namespace Cirtuo.RetailProcurementSystem.Application.StoreItems.Models;

public class StoreItemDto
{
    public int Id { get; set; }
    public string Sku { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public StoreItemCategoryDto Category { get; set; }
    
    public StoreItemDto(int id, string sku, string name, string description, StoreItemCategoryDto category)
    {
        Id = id;
        Sku = sku;
        Name = name;
        Description = description;
        Category = category;
    }
}