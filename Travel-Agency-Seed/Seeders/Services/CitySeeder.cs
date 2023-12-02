using Travel_Agency_DataBase;
using Travel_Agency_Domain;

namespace Travel_Agency_Seed.Seeders.Services;

public class CitySeeder : SeederBase<City>
{
    public CitySeeder() : base("Services/city")
    {
    }

    protected override async Task ConfigureSeed(TravelAgencyContext dbContext) => await SingleData(dbContext);
}