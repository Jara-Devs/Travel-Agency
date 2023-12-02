using Microsoft.EntityFrameworkCore;
using Travel_Agency_DataBase;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_Seed.Seeders.Services;

public class HotelSeeder : SeederBase<Hotel>
{
    public HotelSeeder() : base("Services/hotel")
    {
    }

    protected override async Task ConfigureSeed(TravelAgencyContext dbContext)
    {
        foreach (var item in Data)
        {
            var image = await dbContext.Images.Where(x => x.Name == item.Image.Name).SingleOrDefaultAsync();
            if (image is null)
                throw new Exception($"Image with name {item.Image.Name} not found");

            var place = await dbContext.TouristPlaces.Where(x => x.Name == item.TouristPlace.Name)
                .SingleOrDefaultAsync();
            if (place is null)
                throw new Exception($"TouristPlace with name {item.TouristPlace.Name} not found");

            item.Image = image;
            item.TouristPlace = place;
        }

        await SingleData(dbContext);
    }
}