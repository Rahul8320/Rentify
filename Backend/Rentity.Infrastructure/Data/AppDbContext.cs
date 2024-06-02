using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rentify.Domain.Entities;
using Rentify.Domain.Enums;

namespace Rentity.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Property> Properties { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        SeedRoles(builder);
    }

    private static void SeedRoles(ModelBuilder builder)
    {
        builder.Entity<IdentityRole>().HasData
        (
            // Seed Roles to Database
            new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = UserRole.Buyer.ToString(), NormalizedName = "BUYER" },
            new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = UserRole.Seller.ToString(), NormalizedName = "SELLER" }
        );
    }
}
