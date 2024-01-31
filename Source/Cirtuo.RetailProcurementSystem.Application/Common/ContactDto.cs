namespace Cirtuo.RetailProcurementSystem.Application.Common;

public class ContactDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    
    public ContactDto(int id, string email, string phone)
    {
        Id = id;
        Email = email;
        Phone = phone;
    }
}