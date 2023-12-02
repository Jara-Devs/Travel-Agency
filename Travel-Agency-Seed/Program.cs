using Microsoft.EntityFrameworkCore;
using Travel_Agency_DataBase;
using Travel_Agency_Seed.Seeders;

var connectionString = "server=localhost;port=3306;database=TravelAgency;user=travelagency;password=travelagency";

var serverVersion = new MySqlServerVersion(new Version(8, 0, 33));

var optionsBuilder = new DbContextOptionsBuilder<TravelAgencyContext>();
optionsBuilder.UseMySql(connectionString, serverVersion);

await using (var context = new TravelAgencyContext(optionsBuilder.Options))
{
    try
    {
        if (!await context.Database.EnsureCreatedAsync())
            await context.Database.MigrateAsync();
        
        await SeedDatabase(context);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while migrating and seeding the database {ex}");
    }
}

static async Task SeedDatabase(TravelAgencyContext context)
{
    var seeder = new ImageSeeder();
    await seeder.Execute(context);
}