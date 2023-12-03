using Microsoft.EntityFrameworkCore;
using Travel_Agency_DataBase;
using Travel_Agency_Domain.Users;
using Travel_Agency_Logic;

namespace Travel_Agency_Seed.Seeders.Users;

public class UsersAgencySeeder : SeederBase<UserAgency>
{
    public UsersAgencySeeder() : base("Users/userAgency")
    {
    }

    protected override async Task ConfigureSeed(TravelAgencyContext dbContext)
    {
        foreach (var item in Data)
        {
            var agency = await dbContext.Agencies.Where(x => x.Name == item.Agency.Name)
                .SingleOrDefaultAsync();
            if (agency is null) throw new Exception($"Agency with name {item.Agency.Name} not found");

            item.Agency = agency;
            item.Password = SecurityService.EncryptPassword(item.Password);
        }

        dbContext.AddRange(Data);
        await dbContext.SaveChangesAsync();
    }
}