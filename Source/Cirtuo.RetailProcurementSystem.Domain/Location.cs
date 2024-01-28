namespace Cirtuo.RetailProcurementSystem.Domain;

public class Location
{
    public int Id { get; private set; }
    public string Address { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string ZipCode { get; private set; }
    
    public Location() { }

    public Location(string address, string city, string state, string zipCode)
    {
        Address = address;
        City = city;
        State = state;
        ZipCode = zipCode;
    }
}