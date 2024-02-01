namespace Cirtuo.RetailProcurementSystem.Application.Common.Models;

public class ManagerDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public int ContactId { get; set; }
    public ContactDto Contact { get; set; }

    public ManagerDto(int id, string name, int contactId)
    {
        Id = id;
        Name = name;
        ContactId = contactId;
    }
}