using Microsoft.EntityFrameworkCore;
using Travel_Agency_DataBase;
using Travel_Agency_Domain.Users;

namespace Travel_Agency_Seed.Seeders.Users;

public class UsersAgencySeeder : SeederBase<UserAgency>
{
    public UsersAgencySeeder() : base("Users/userAgency")
    {
    }

    protected override async Task ConfigureSeed(TravelAgencyContext dbContext)
    {
        var agencies = await dbContext.Agencies.ToListAsync();

        foreach (var (user, agency) in Data.Zip(agencies))
        {
            user.Agency = agency;
        }
        
        dbContext.AddRange(Data);
        await dbContext.SaveChangesAsync();
    }
}