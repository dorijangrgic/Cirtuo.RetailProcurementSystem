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
    }
}