using Travel_Agency_DataBase;
using Travel_Agency_Domain;

namespace Travel_Agency_Seed.Seeders;

public class AgencySeeder : SeederBase<Agency>
{
    public AgencySeeder() : base("agency")
    {
    }

    protected override async Task ConfigureSeed(TravelAgencyContext dbContext) => await SingleData(dbContext);
}