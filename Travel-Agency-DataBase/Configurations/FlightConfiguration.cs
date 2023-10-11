using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_DataBase.Configurations;

public class FlightConfiguration : EntityConfiguration<Flight>
{
    public override void ConfigureEntity(EntityTypeBuilder<Flight> builder)
    {
        builder.HasOne(f => f.Place1).WithMany(p => p.Flights1).HasForeignKey(f => f.Place1Id);
        builder.HasOne(f => f.Place2).WithMany(p => p.Flights2).HasForeignKey(f => f.Place2Id);
    }
}