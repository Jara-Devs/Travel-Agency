using Microsoft.EntityFrameworkCore;
using Travel_Agency_DataBase;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_Seed.Seeders.Services;

public class TouristPlaceSeeder : SeederBase<TouristPlace>
{
    public TouristPlaceSeeder() : base("Services/touristPlace")
    {
    }

    protected override async Task ConfigureSeed(TravelAgencyContext dbContext)
    {
        foreach (var item in Data)
        {
            var image = await dbContext.Images.Where(x => x.Name == item.Image.Name).SingleOrDefaultAsync();
            if (image is null)
                throw new Exception($"Image with name {item.Name} not found");

            var city = await dbContext.Cities.Where(x => x.Name == item.City.Name && x.Country == item.City.Country)
                .SingleOrDefaultAsync();
            if (city is null)
                throw new Exception($"City with name {item.City.Name} and country {item.City.Country} not found");

            item.Image = image;
            item.City = city;
        }

        await SingleData(dbContext);
    }
}