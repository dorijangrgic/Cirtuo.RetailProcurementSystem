using Cirtuo.RetailProcurementSystem.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cirtuo.RetailProcurementSystem.Persistence.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.ItemPrice).IsRequired().HasPrecision(1000, 5);

        builder.HasOne(x => x.Order)
            .WithMany(x => x.OrderItems)
            .HasForeignKey(x => x.OrderId);
        
        builder.HasOne(x => x.SupplierStoreItem)
            .WithMany(x => x.OrderItems)
            .HasForeignKey(x => x.SupplierStoreItemId);
    }
}