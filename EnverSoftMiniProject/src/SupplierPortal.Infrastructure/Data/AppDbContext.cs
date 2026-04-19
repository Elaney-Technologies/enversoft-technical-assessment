using Microsoft.EntityFrameworkCore;
using SupplierPortal.Domain.Entities;

namespace SupplierPortal.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Supplier> Suppliers => Set<Supplier>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.ToTable("Suppliers");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name).HasMaxLength(200).IsRequired();
            entity.Property(x => x.TelephoneNo).HasMaxLength(50).IsRequired();
            entity.Property(x => x.Code).IsRequired();
            entity.Property(x => x.CreatedUtc).HasDefaultValueSql("GETUTCDATE()");
            entity.HasIndex(x => x.Name).IsUnique();
        });
    }
}
