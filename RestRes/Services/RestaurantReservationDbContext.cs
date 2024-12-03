using Microsoft.EntityFrameworkCore;
using RestRes.Models;

namespace RestRes.Services
{
  public class RestaurantReservationDbContext : DbContext
  {
    public RestaurantReservationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<Restaurant>();
      modelBuilder.Entity<Reservation>();
    }
  }
}
