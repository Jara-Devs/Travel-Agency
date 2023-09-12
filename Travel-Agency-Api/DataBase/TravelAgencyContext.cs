using Microsoft.EntityFrameworkCore;

namespace Travel_Agency_Api.DataBase;

public class TravelAgencyContext : DbContext
{
    
    public TravelAgencyContext(DbContextOptions<TravelAgencyContext> options) : base(options)
    {
    }
}