using Microsoft.EntityFrameworkCore;

namespace HeartHousingAngular.Models;

public class RentalDbContext : DbContext
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
