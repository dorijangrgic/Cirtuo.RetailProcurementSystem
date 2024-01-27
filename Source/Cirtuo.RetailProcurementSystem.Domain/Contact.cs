namespace Cirtuo.RetailProcurementSystem.Domain;

public class Contact
{
    public int Id { get; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    
    public Contact(int id, string email, string phone)
    {
        Id = id;
        Email = email;
        Phone = phone;
    }
}