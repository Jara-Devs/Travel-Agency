using Microsoft.EntityFrameworkCore;
using Travel_Agency_DataBase;
using Travel_Agency_Seed.Seeders;
using Travel_Agency_Seed.Seeders.Services;
using Travel_Agency_Seed.Seeders.Users;

var connectionString = "server=localhost;port=3306;database=TravelAgency;user=travelagency;password=travelagency";

var serverVersion = new MySqlServerVersion(new Version(8, 0, 33));

var optionsBuilder = new DbContextOptionsBuilder<TravelAgencyContext>();
optionsBuilder.UseMySql(connectionString, serverVersion);

await using (var context = new TravelAgencyContext(optionsBuilder.Options))
{
    try
    {
        await SeedDatabase(context);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while seeding the database {ex}");
    }
}

static async Task SeedDatabase(TravelAgencyContext context)
{
    // Create Seeders
    var image = new ImageSeeder();
    var agency = new AgencySeeder();

    var tourist = new TouristSeeder();
    var userAgency = new UsersAgencySeeder();

    var touristPlace = new TouristPlaceSeeder();
    var touristActivity = new TouristActivitySeeder();
    var hotel = new HotelSeeder();
    var excursion = new ExcursionSeeder();
    var flight = new FlightSeeder();
    var facility = new FacilitySeeder();
    var city = new CitySeeder();

    // Execute
    await image.Execute(context);
    await agency.Execute(context);

    await tourist.Execute(context);
    await userAgency.Execute(context);

    await touristPlace.Execute(context);
    await touristActivity.Execute(context);
    await hotel.Execute(context);
    await flight.Execute(context);
    await excursion.Execute(context);
    await facility.Execute(context);
    await city.Execute(context);
}