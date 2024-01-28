namespace Cirtuo.RetailProcurementSystem.Domain;

public class Retailer
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public int LocationId { get; private set; }
    public int ContactId { get; private set; }
    public int ManagerId { get; private set; }
    
    public Location Location { get; private set; }
    public Contact Contact { get; private set; }
    public Manager Manager { get; private set; }
    public ICollection<SupplierRetailer> SupplierRetailers { get; private set; }
    public ICollection<Order> Orders { get; private set; }
    
    public Retailer() { }
    
    public Retailer(string name, int locationId, int contactId, int managerId)
    {
        Name = name;
        LocationId = locationId;
        ContactId = contactId;
        ManagerId = managerId;
        
        SupplierRetailers ??= new List<SupplierRetailer>();
        Orders ??= new List<Order>();
    }
}