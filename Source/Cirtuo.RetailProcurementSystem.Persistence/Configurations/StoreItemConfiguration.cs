using Cirtuo.RetailProcurementSystem.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cirtuo.RetailProcurementSystem.Persistence.Configurations;

public class StoreItemConfiguration : IEntityTypeConfiguration<StoreItem>
{
    public void Configure(EntityTypeBuilder<StoreItem> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Sku).IsUnique();

        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.Sku).IsRequired().HasMaxLength(64);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.Category).IsRequired();
    }
}