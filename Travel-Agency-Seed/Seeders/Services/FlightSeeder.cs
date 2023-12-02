using Microsoft.EntityFrameworkCore;
using Travel_Agency_DataBase;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_Seed.Seeders.Services;

public class FlightSeeder : SeederBase<Flight>
{
    public FlightSeeder() : base("Services/flight")
    {
    }

    protected override async Task ConfigureSeed(TravelAgencyContext dbContext)
    {
        foreach (var item in Data)
        {
            var origin = await dbContext.Cities.Where(x => x.Name == item.Origin.Name).SingleOrDefaultAsync();
            if (origin is null)
                throw new Exception($"City with name {item.Origin.Name} not found");

            var destination = await dbContext.Cities.Where(x => x.Name == item.Destination.Name)
                .SingleOrDefaultAsync();
            if (destination is null)
                throw new Exception($"City with name {item.Origin.Name} not found");


            item.Origin = origin;
            item.Destination = destination;
        }

        await SingleData(dbContext);
    }
}