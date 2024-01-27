namespace Cirtuo.RetailProcurementSystem.Domain;

public class Manager
{
    public int Id { get; }
    public string Name { get; private set; }
    
    public int ContactId { get; private set; }
    public Contact Contact { get; private set; }
    
    public Manager(int id, string name, int contactId)
    {
        Id = id;
        Name = name;
        ContactId = contactId;
    }
    
    public void UpdateContact(Contact contact)
    {
        Contact = contact;
    }
}