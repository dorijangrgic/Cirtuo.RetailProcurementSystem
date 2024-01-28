namespace Cirtuo.RetailProcurementSystem.Domain;

public class Contact
{
    public int Id { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    
    public Contact() { }
    
    public Contact(string email, string phone)
    {
        Email = email;
        Phone = phone;
    }
}