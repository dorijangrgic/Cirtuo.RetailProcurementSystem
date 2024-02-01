using Cirtuo.RetailProcurementSystem.Application.Common;
using Cirtuo.RetailProcurementSystem.Application.Common.Models;
using Cirtuo.RetailProcurementSystem.Application.Suppliers.Models;

namespace Cirtuo.RetailProcurementSystem.Application.Test.Builders;

public class SupplierDtoBuilder
{
    private int _id;
    private string _name;
    private LocationDto _location;
    private ContactDto _contact;

    public static SupplierDtoBuilder Default()
    {
        return new SupplierDtoBuilder()
            .WithId(1)
            .WithName("Supplier 1")
            .WithLocation(LocationDtoBuilder.Default().Build())
            .WithContact(ContactDtoBuilder.Default().Build());
    }
    
    public SupplierDtoBuilder WithId(int id)
    {
        _id = id;
        return this;
    }
    
    public SupplierDtoBuilder WithName(string name)
    {
        _name = name;
        return this;
    }
    
    public SupplierDtoBuilder WithLocation(LocationDto location)
    {
        _location = location;
        return this;
    }
    
    public SupplierDtoBuilder WithContact(ContactDto contact)
    {
        _contact = contact;
        return this;
    }
    
    public SupplierDto Build() => new(_id, _name, _location, _contact);
}