using InventoryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryService.Persistence.Configurations;

public class StockConfiguration : IEntityTypeConfiguration<StockEntry>
{
    public void Configure(EntityTypeBuilder<StockEntry> builder)
    {
        builder.ToTable("ItemStock");

        builder.Property(i => i.Price)
            .IsRequired(true);
    }
}
