namespace Cirtuo.RetailProcurementSystem.Domain;

public class SupplierRetailer
{
    public int Id { get; private set; }
    public int SupplierId { get; private set; }
    public int RetailerId { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    
    public Supplier Supplier { get; private set; }
    public Retailer Retailer { get; private set; }
    
    public SupplierRetailer() { }
    
    public SupplierRetailer(int supplierId, int retailerId, DateTime startDate, DateTime endDate)
    {
        SupplierId = supplierId;
        RetailerId = retailerId;
        StartDate = startDate;
        EndDate = endDate;
    }
    
    public void UpdateEndDate(DateTime endDate)
    {
        EndDate = endDate;
    }
}