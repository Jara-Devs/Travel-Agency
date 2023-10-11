using Microsoft.EntityFrameworkCore;
using Travel_Agency_Api.Models.User;
using Travel_Agency_Api.Models;
using Travel_Agency_Api.Models.Offers;
using Travel_Agency_Api.Models.Services;

namespace Travel_Agency_Api.DataBase;

public class TravelAgencyContext : DbContext
{
    public TravelAgencyContext(DbContextOptions<TravelAgencyContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TouristPlace>().OwnsOne(h => h.Address);
    }

    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Excursion> Excursions { get; set; } = null!;

    public DbSet<Hotel> Hotels { get; set; } = null!;

    public DbSet<Flight> Flights { get; set; } = null!;

    public DbSet<TouristPlace> TouristPlaces { get; set; } = null!;

    public DbSet<TouristActivity> TouristActivities { get; set; } = null!;

    public DbSet<HotelOffer> HotelOffers { get; set; } = null!;

    public DbSet<FlightOffer> FlightOffers { get; set; } = null!;

    public DbSet<ExcursionOffer> ExcursionOffers { get; set; } = null!;
}