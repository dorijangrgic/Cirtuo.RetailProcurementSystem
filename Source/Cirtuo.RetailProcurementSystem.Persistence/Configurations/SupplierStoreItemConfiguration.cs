using Cirtuo.RetailProcurementSystem.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cirtuo.RetailProcurementSystem.Persistence.Configurations;

public class SupplierStoreItemConfiguration : IEntityTypeConfiguration<SupplierStoreItem>
{
    public void Configure(EntityTypeBuilder<SupplierStoreItem> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.StartDate).IsRequired();
        builder.Property(x => x.EndDate).IsRequired();
        builder.Property(x => x.Quarter).IsRequired();
        builder.Property(x => x.Year).IsRequired();
        builder.Property(x => x.ItemPrice).IsRequired().HasPrecision(1000, 5);
        builder.Property(x => x.SoldItems).IsRequired();

        builder.HasOne(x => x.Supplier)
            .WithMany(x => x.SupplierStoreItems)
            .HasForeignKey(x => x.SupplierId);

        builder.HasOne(x => x.StoreItem)
            .WithMany(x => x.SupplierStoreItems)
            .HasForeignKey(x => x.StoreItemId);
    }
}