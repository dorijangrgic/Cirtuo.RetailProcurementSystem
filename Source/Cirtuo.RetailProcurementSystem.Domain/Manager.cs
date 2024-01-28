namespace Cirtuo.RetailProcurementSystem.Domain;

public class Manager
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    
    public int ContactId { get; private set; }
    public Contact Contact { get; private set; }
    
    public Manager() { }

    public Manager(string name, int contactId)
    {
        Name = name;
        ContactId = contactId;
    }
}