using Cirtuo.RetailProcurementSystem.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cirtuo.RetailProcurementSystem.Persistence.Configurations;

public class SupplierRetailerConfiguration : IEntityTypeConfiguration<SupplierRetailer>
{
    public void Configure(EntityTypeBuilder<SupplierRetailer> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.SupplierId).IsRequired();
        builder.Property(x => x.RetailerId).IsRequired();
        builder.Property(x => x.StartDate).IsRequired();
        builder.Property(x => x.EndDate);

        builder.HasOne(x => x.Supplier)
            .WithMany(x => x.SupplierRetailers)
            .HasForeignKey(x => x.SupplierId);
        
        builder.HasOne(x => x.Retailer)
            .WithMany(x => x.SupplierRetailers)
            .HasForeignKey(x => x.RetailerId);
    }
}