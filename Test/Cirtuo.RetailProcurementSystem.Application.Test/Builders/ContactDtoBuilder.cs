using Cirtuo.RetailProcurementSystem.Application.Common;
using Cirtuo.RetailProcurementSystem.Application.Common.Models;

namespace Cirtuo.RetailProcurementSystem.Application.Test.Builders;

public class ContactDtoBuilder
{
    private int _id;
    private string _email;
    private string _phone;

    public static ContactDtoBuilder Default()
    {
        return new ContactDtoBuilder()
            .WithId(1)
            .WithEmail("info@cirtuo.com")
            .WithPhone("+1 123 456 7890");
    }
    
    public ContactDtoBuilder WithId(int id)
    {
        _id = id;
        return this;
    }
    
    public ContactDtoBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }
    
    public ContactDtoBuilder WithPhone(string phone)
    {
        _phone = phone;
        return this;
    }
    
    public ContactDto Build() => new(_id, _email, _phone);
}