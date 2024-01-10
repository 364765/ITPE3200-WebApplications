using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HeartHousing.Models;

public class RentalDbContext : IdentityDbContext
{
    public RentalDbContext(DbContextOptions<RentalDbContext> options) : base(options) 
    {
        Database.EnsureCreated();
    }   

    public DbSet<Rental> Rentals { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }
}
