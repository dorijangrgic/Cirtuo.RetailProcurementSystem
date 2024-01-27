using Cirtuo.RetailProcurementSystem.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cirtuo.RetailProcurementSystem.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RetailerId).IsRequired();
        builder.Property(x => x.OrderDate).IsRequired();
        builder.Property(x => x.DeliveryDate);
        builder.Property(x => x.PaymentDate);
        builder.Property(x => x.TotalPrice).IsRequired();

        builder.HasOne(x => x.Retailer)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.RetailerId);
    }
}