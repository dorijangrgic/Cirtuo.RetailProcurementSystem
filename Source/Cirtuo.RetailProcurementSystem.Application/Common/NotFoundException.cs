namespace Cirtuo.RetailProcurementSystem.Application.Common;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }
}