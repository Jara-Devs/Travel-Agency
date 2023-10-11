using Microsoft.EntityFrameworkCore;
using Travel_Agency_DataBase.Configurations;
using Travel_Agency_Domain;
using Travel_Agency_Domain.Offers;
using Travel_Agency_Domain.Services;
using Travel_Agency_Domain.User;

namespace Travel_Agency_DataBase;

public class TravelAgencyContext : DbContext
{
    public TravelAgencyContext(DbContextOptions<TravelAgencyContext> options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserAgencyConfiguration());
    }

    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Tourist> Tourists { get; set; } = null!;

    public DbSet<Agency> Agencies { get; set; } = null!;

    public DbSet<UserAgency> UserAgencies { get; set; } = null!;

    // public DbSet<Excursion> Excursions { get; set; } = null!;
    //
    // public DbSet<Hotel> Hotels { get; set; } = null!;
    //
    // public DbSet<Flight> Flights { get; set; } = null!;
    //
    // public DbSet<TouristPlace> TouristPlaces { get; set; } = null!;
    //
    // public DbSet<TouristActivity> TouristActivities { get; set; } = null!;
    //
    // public DbSet<HotelOffer> HotelOffers { get; set; } = null!;
    //
    // public DbSet<FlightOffer> FlightOffers { get; set; } = null!;
    //
    // public DbSet<ExcursionOffer> ExcursionOffers { get; set; } = null!;
}