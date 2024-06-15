using HataPropertyService.Models;
using Microsoft.EntityFrameworkCore;

namespace HataPropertyService.Data;

public class PropertyContext : DbContext
{
    public PropertyContext(DbContextOptions<PropertyContext> options) : base(options) { }

    public DbSet<Property> Properties { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Property>().HasKey(p => p.Id);
        base.OnModelCreating(modelBuilder);
    }
}