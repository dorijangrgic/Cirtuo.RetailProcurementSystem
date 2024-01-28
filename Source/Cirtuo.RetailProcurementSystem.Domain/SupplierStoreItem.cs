namespace Cirtuo.RetailProcurementSystem.Domain;

public class SupplierStoreItem
{
    public int Id { get; private set; }
    public int SupplierId { get; private set; }
    public int StoreItemId { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public int Quarter { get; private set; }
    public int Year { get; private set; }
    public decimal ItemPrice { get; private set; }
    public int SoldItems { get; private set; }
    
    public Supplier Supplier { get; private set; }
    public StoreItem StoreItem { get; private set; }
    public ICollection<OrderItem> OrderItems { get; private set; }
    
    public SupplierStoreItem() { }

    public SupplierStoreItem(
        int supplierId,
        int storeItemId,
        DateTime startDate,
        DateTime endDate,
        int quarter,
        int year,
        decimal itemPrice,
        int soldItems
    )
    {
        SupplierId = supplierId;
        StoreItemId = storeItemId;
        StartDate = startDate;
        EndDate = endDate;
        Quarter = quarter;
        Year = year;
        ItemPrice = itemPrice;
        SoldItems = soldItems;
        
        OrderItems ??= new List<OrderItem>();
    }
}