using Travel_Agency_DataBase;
using Travel_Agency_Domain.Images;

namespace Travel_Agency_Seed.Seeders;

public class ImageSeeder : SeederBase<Image>
{
    public ImageSeeder() : base("image")
    {
    }

    protected override async Task ConfigureSeed(TravelAgencyContext dbContext) => await SingleData(dbContext);
}