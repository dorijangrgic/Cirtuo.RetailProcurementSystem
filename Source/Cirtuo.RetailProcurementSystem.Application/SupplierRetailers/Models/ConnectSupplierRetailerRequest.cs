namespace Cirtuo.RetailProcurementSystem.Application.SupplierRetailers.Models;

public class ConnectSupplierRetailerRequest
{
    public int RetailerId { get; set; }
    public List<int> SupplierIds { get; set; }
    
    public ConnectSupplierRetailerRequest(int retailerId, List<int> supplierIds)
    {
        RetailerId = retailerId;
        SupplierIds = supplierIds;
    }
}