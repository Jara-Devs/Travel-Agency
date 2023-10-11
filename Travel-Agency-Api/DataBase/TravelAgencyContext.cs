using Microsoft.EntityFrameworkCore;
using Travel_Agency_Api.Models.User;

namespace Travel_Agency_Api.DataBase;

public class TravelAgencyContext : DbContext
{
    public TravelAgencyContext(DbContextOptions<TravelAgencyContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
}