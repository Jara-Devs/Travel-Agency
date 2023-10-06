using Microsoft.EntityFrameworkCore;
using Travel_Agency_Api.Models;
using Travel_Agency_Api.Models.Services;

namespace Travel_Agency_Api.DataBase;

public class TravelAgencyContext : DbContext
{
    public TravelAgencyContext(DbContextOptions<TravelAgencyContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Excursion> Excursions { get; set; } = null!;

    public DbSet<Hotel> Hotels { get; set; } = null!;

    public DbSet<Flight> Flights { get; set; } = null!;

    public DbSet<TouristPlace> TouristPlaces { get; set; } = null!;

    public DbSet<TouristActivity> TouristActivities { get; set; } = null!;
}