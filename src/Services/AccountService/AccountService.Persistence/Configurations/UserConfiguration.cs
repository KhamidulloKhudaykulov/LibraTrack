using AccountService.Domain.Entities;
using AccountService.Domain.ValueObjects.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountService.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName)
            .HasConversion(x => x!.Value, v => FirstName.Create(v).Value);

        builder.Property(x => x.LastName)
            .HasConversion(x => x!.Value, v => LastName.Create(v).Value);

        builder.Property(x => x.Email)
            .HasConversion(x => x!.Value, v => Email.Create(v).Value);

        builder.Property(x => x.PhoneNumber)
            .HasConversion(x => x!.Value, v => PhoneNumber.Create(v).Value);

        builder.Property(x => x.PassportNumber)
            .HasConversion(x => x!.Value, v => PassportNumber.Create(v).Value);
    }
}
