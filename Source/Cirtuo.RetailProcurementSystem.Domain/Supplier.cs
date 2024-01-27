namespace Cirtuo.RetailProcurementSystem.Domain;

public class Supplier
{
    public int Id { get; }
    public string Name { get; private set; }
    public int LocationId { get; private set; }
    public int ContactId { get; private set; }
    
    public Location Location { get; private set; }
    public Contact Contact { get; private set; }
    public ICollection<SupplierRetailer> SupplierRetailers { get; private set; }
    public ICollection<SupplierStoreItem> SupplierStoreItems { get; private set; }

    public Supplier(int id, string name, int locationId, int contactId)
    {
        Id = id;
        Name = name;
        LocationId = locationId;
        ContactId = contactId;
        
        SupplierRetailers ??= new List<SupplierRetailer>();
        SupplierStoreItems ??= new List<SupplierStoreItem>();
    }
}