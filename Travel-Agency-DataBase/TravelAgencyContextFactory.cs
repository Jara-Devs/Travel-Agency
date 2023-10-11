using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Travel_Agency_DataBase;

internal class TravelAgencyContextFactory : IDesignTimeDbContextFactory<TravelAgencyContext>
{
    public TravelAgencyContext CreateDbContext(string[] args)
    {
        var dbContextBuilder = new DbContextOptionsBuilder<TravelAgencyContext>();

        var serverVersion = new MySqlServerVersion(new Version(8, 0, 33));
        var connectionString =
            "server=localhost;port=3306;database=TravelAgency;user=travelagency;password=travelagency";

        dbContextBuilder.UseMySql(connectionString, serverVersion);
        return new TravelAgencyContext(dbContextBuilder.Options);
    }
}