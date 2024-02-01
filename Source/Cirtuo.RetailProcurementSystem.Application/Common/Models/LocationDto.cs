namespace Cirtuo.RetailProcurementSystem.Application.Common.Models;

public class LocationDto
{
    public int Id { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    
    public LocationDto(int id, string address, string city, string state, string zipCode)
    {
        Id = id;
        Address = address;
        City = city;
        State = state;
        ZipCode = zipCode;
    }
}