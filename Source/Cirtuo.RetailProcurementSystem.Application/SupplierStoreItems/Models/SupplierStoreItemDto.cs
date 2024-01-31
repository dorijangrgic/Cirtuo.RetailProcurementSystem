using Cirtuo.RetailProcurementSystem.Application.StoreItems;
using Cirtuo.RetailProcurementSystem.Application.StoreItems.Models;
using Cirtuo.RetailProcurementSystem.Application.Suppliers;
using Cirtuo.RetailProcurementSystem.Application.Suppliers.Models;

namespace Cirtuo.RetailProcurementSystem.Application.SupplierStoreItems.Models;

public class SupplierStoreItemDto
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Quarter { get; set; }
    public int Year { get; set; }
    public decimal ItemPrice { get; set; }
    public int SoldItems { get; set; }
    public SupplierDto Supplier { get; set; }
    public StoreItemDto StoreItem { get; set; }
    
    public SupplierStoreItemDto(
        int id,
        DateTime startDate,
        DateTime endDate,
        int quarter,
        int year,
        decimal itemPrice,
        int soldItems,
        SupplierDto supplier,
        StoreItemDto storeItem
    )
    {
        Id = id;
        StartDate = startDate;
        EndDate = endDate;
        Quarter = quarter;
        Year = year;
        ItemPrice = itemPrice;
        SoldItems = soldItems;
        Supplier = supplier;
        StoreItem = storeItem;
    }
}