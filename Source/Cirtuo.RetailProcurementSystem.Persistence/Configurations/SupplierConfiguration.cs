using Cirtuo.RetailProcurementSystem.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cirtuo.RetailProcurementSystem.Persistence.Configurations;

public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.LocationId).IsRequired();
        builder.Property(x => x.ContactId).IsRequired();
        
        builder.HasOne(x => x.Location).WithMany().HasForeignKey(x => x.LocationId);
        builder.HasOne(x => x.Contact).WithMany().HasForeignKey(x => x.ContactId);
    }
}