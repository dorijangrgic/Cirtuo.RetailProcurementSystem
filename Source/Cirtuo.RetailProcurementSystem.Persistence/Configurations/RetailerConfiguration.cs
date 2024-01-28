using Cirtuo.RetailProcurementSystem.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cirtuo.RetailProcurementSystem.Persistence.Configurations;

public class RetailerConfiguration : IEntityTypeConfiguration<Retailer>
{
    public void Configure(EntityTypeBuilder<Retailer> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

        builder.HasOne(x => x.Location).WithMany().HasForeignKey(x => x.LocationId);
        builder.HasOne(x => x.Contact).WithMany().HasForeignKey(x => x.ContactId);
        builder.HasOne(x => x.Manager).WithMany().HasForeignKey(x => x.ManagerId);
    }
}