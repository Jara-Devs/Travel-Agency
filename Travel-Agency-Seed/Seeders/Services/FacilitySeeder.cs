using Travel_Agency_DataBase;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_Seed.Seeders.Services;

public class FacilitySeeder : SeederBase<Facility>
{
    public FacilitySeeder() : base("Services/facility")
    {
    }

    protected override async Task ConfigureSeed(TravelAgencyContext dbContext) => await SingleData(dbContext);
}