using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalService.Domain.Entities;

namespace RentalService.Persistence.Configurations;

public class RentalRecordConfiguration : IEntityTypeConfiguration<RentalRecord>
{
    public void Configure(EntityTypeBuilder<RentalRecord> builder)
    {
        builder.ToTable("RentalRecords");

        builder.HasKey(x => x.Id);
    }
}
