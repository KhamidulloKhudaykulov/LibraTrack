using Microsoft.EntityFrameworkCore;
using NotificationService.Persistence.Extensions;

namespace NotificationService.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
}
