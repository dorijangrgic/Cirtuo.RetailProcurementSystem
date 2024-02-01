using Cirtuo.RetailProcurementSystem.Application.Common;

namespace Cirtuo.RetailProcurementSystem.Application.Test.Builders;

public class LocationDtoBuilder
{
    private int _id;
    private string _address;
    private string _city;
    private string _state;
    private string _zipCode;
    
    public static LocationDtoBuilder Default()
    {
        return new LocationDtoBuilder()
            .WithId(1)
            .WithAddress("123 Main St")
            .WithCity("New York")
            .WithState("NY")
            .WithZipCode("10001");
    }
    
    public LocationDtoBuilder WithId(int id)
    {
        _id = id;
        return this;
    }
    
    public LocationDtoBuilder WithAddress(string address)
    {
        _address = address;
        return this;
    }
    
    public LocationDtoBuilder WithCity(string city)
    {
        _city = city;
        return this;
    }
    
    public LocationDtoBuilder WithState(string state)
    {
        _state = state;
        return this;
    }
    
    public LocationDtoBuilder WithZipCode(string zipCode)
    {
        _zipCode = zipCode;
        return this;
    }
    
    public LocationDto Build() => new(_id, _address, _city, _state, _zipCode);
}