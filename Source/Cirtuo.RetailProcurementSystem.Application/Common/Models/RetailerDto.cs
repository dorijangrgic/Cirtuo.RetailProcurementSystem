using Cirtuo.RetailProcurementSystem.Application.SupplierRetailers.Models;

namespace Cirtuo.RetailProcurementSystem.Application.Common.Models;

public class RetailerDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int LocationId { get; set; }
    public int ContactId { get; set; }
    public int ManagerId { get; set; }
    
    public LocationDto Location { get; set; }
    public ContactDto Contact { get; set; }
    public ManagerDto Manager { get; set; }
    public IEnumerable<SupplierRetailerDto> SupplierRetailers { get; set; }
    public IEnumerable<OrderDto> Orders { get; set; }

    public RetailerDto(int id, string name, int locationId, int contactId, int managerId)
    {
        Id = id;
        Name = name;
        LocationId = locationId;
        ContactId = contactId;
        ManagerId = managerId;
    }
}