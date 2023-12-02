using Microsoft.EntityFrameworkCore;
using Travel_Agency_DataBase;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_Seed.Seeders.Services;

public class TouristActivitySeeder : SeederBase<TouristActivity>
{
    public TouristActivitySeeder() : base("Services/touristActivity")
    {
    }

    protected override async Task ConfigureSeed(TravelAgencyContext dbContext)
    {
        foreach (var item in Data)
        {
            var image = await dbContext.Images.Where(x => x.Name == item.Image.Name).SingleOrDefaultAsync();
            if (image is null)
                throw new Exception($"Image with name {item.Name} not found");

            item.Image = image;
        }

        await SingleData(dbContext);
    }
}