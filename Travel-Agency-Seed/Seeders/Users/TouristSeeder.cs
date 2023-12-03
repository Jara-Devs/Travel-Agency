using Travel_Agency_DataBase;
using Travel_Agency_Domain;
using Travel_Agency_Domain.Users;
using Travel_Agency_Logic;

namespace Travel_Agency_Seed.Seeders.Users;

public class TouristSeeder : SeederBase<Tourist>
{
    public TouristSeeder() : base("Users/tourist")
    {
    }

    protected override async Task ConfigureSeed(TravelAgencyContext dbContext)
    {
        foreach (var item in Data)
        {
            item.Password = SecurityService.EncryptPassword(item.Password);
        }

        await SingleData(dbContext);
    }
}