using Cirtuo.RetailProcurementSystem.Domain;
using Cirtuo.RetailProcurementSystem.Persistence.Seeders;
using Microsoft.EntityFrameworkCore;

namespace Cirtuo.RetailProcurementSystem.Persistence;

public class RetailProcurementDbContext : DbContext
{
    public RetailProcurementDbContext(DbContextOptions<RetailProcurementDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RetailProcurementDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
        
        var dataSeeder = new DataSeeder();
        modelBuilder.Entity<Location>().HasData(dataSeeder.Locations);
        modelBuilder.Entity<Contact>().HasData(dataSeeder.Contacts);
        modelBuilder.Entity<Manager>().HasData(dataSeeder.Managers);
        modelBuilder.Entity<Retailer>().HasData(dataSeeder.Retailers);
        modelBuilder.Entity<Supplier>().HasData(dataSeeder.Suppliers);
        modelBuilder.Entity<SupplierRetailer>().HasData(dataSeeder.SuppliersRetailers);
        modelBuilder.Entity<StoreItem>().HasData(dataSeeder.StoreItems);
        modelBuilder.Entity<SupplierStoreItem>().HasData(dataSeeder.SupplierStoreItems);
        modelBuilder.Entity<Order>().HasData(dataSeeder.Orders);
        modelBuilder.Entity<OrderItem>().HasData(dataSeeder.OrderItems);
    }
}