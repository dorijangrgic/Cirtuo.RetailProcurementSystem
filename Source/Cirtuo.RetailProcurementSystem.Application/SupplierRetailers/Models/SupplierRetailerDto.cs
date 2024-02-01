using Cirtuo.RetailProcurementSystem.Application.Common;
using Cirtuo.RetailProcurementSystem.Application.Common.Models;
using Cirtuo.RetailProcurementSystem.Application.Suppliers.Models;

namespace Cirtuo.RetailProcurementSystem.Application.SupplierRetailers.Models;

public class SupplierRetailerDto
{
    public int Id { get; set; }
    public int SupplierId { get; set; }
    public int RetailerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    
    public SupplierDto Supplier { get; set; }
    public RetailerDto Retailer { get; set; }

    public SupplierRetailerDto(int supplierId, int retailerId, DateTime startDate, DateTime? endDate)
    {
        SupplierId = supplierId;
        RetailerId = retailerId;
        StartDate = startDate;
        EndDate = endDate;
    }
}