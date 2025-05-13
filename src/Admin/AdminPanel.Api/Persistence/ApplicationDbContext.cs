using AdminPanel.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Api.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options) { }

    public DbSet<Admin> Admins { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Admin>()
            .ToTable("Admins");

        modelBuilder
            .Entity<Admin>()
            .Property(a => a.Password)
            .IsRequired(true);

        modelBuilder
            .Entity<Admin>()
            .Property(a => a.Login)
            .IsRequired(true);
    }
}
