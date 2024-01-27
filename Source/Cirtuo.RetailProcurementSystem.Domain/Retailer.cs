namespace Cirtuo.RetailProcurementSystem.Domain;

public class Retailer
{
    public int Id { get; }
    public string Name { get; private set; }
    public int LocationId { get; private set; }
    public int ContactId { get; private set; }
    public int ManagerId { get; private set; }
    
    public Location Location { get; private set; }
    public Contact Contact { get; private set; }
    public Manager Manager { get; private set; }
    public ICollection<SupplierRetailer> SupplierRetailers { get; private set; }
    public ICollection<Order> Orders { get; private set; }
    
    public Retailer(int id, string name, int locationId, int contactId, int managerId)
    {
        Id = id;
        Name = name;
        LocationId = locationId;
        ContactId = contactId;
        ManagerId = managerId;
        
        SupplierRetailers ??= new List<SupplierRetailer>();
        Orders ??= new List<Order>();
    }
}