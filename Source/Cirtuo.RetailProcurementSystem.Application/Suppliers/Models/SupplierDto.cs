using Cirtuo.RetailProcurementSystem.Application.Common;
using Cirtuo.RetailProcurementSystem.Application.Common.Models;

namespace Cirtuo.RetailProcurementSystem.Application.Suppliers.Models;

public class SupplierDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public LocationDto Location { get; set; }
    public ContactDto Contact { get; set; }

    public SupplierDto(int id, string name, LocationDto location, ContactDto contact)
    {
        Id = id;
        Name = name;
        Location = location;
        Contact = contact;
    }
}