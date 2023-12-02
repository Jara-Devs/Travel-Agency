using Travel_Agency_DataBase;
using Travel_Agency_Domain.Images;

namespace Travel_Agency_Seed.Seeders;

public class ImageSeeder : SeederBase<Image>
{
    public ImageSeeder() : base("images")
    {
    }

    protected override async Task ConfigureSeed(TravelAgencyContext dbContext)
    {
        Console.WriteLine(Data[0].Name);
        await dbContext.Images.AddRangeAsync(Data);
        await dbContext.SaveChangesAsync();
    }
}