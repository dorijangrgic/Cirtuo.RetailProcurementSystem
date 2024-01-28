namespace Cirtuo.RetailProcurementSystem.Domain;

public class StoreItem
{
    public int Id { get; private set; }
    public string Sku { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public StoreItemCategory Category { get; private set; }

    public ICollection<SupplierStoreItem> SupplierStoreItems { get; private set; }
    
    public StoreItem() { }
    
    public StoreItem(
        string sku,
        string name,
        string description,
        StoreItemCategory category
    )
    {
        Sku = sku;
        Name = name;
        Description = description;
        Category = category;
        
        SupplierStoreItems ??= new List<SupplierStoreItem>();
    }
}