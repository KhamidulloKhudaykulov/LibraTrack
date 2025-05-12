using BookService.Domain.Entities;
using BookService.Domain.ValueObjects.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookService.Persistence.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .HasConversion(x => x!.Value, v => Title.Create(v).Value);

        builder.Property(x => x.Description)
            .HasConversion(x => x!.Value, v => Description.Create(v).Value);

        builder.Property(x => x.Publisher)
            .HasConversion(x => x!.Value, v => Publisher.Create(v).Value);

        builder.Property(x => x.Price)
            .HasConversion(x => x!.Value, v => Price.Create(v).Value);

        builder.Property(x => x.Author)
            .HasConversion(x => x!.Value, v => Author.Create(v).Value);
    }
}
