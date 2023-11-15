using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_DataBase.Configurations;

public class FlightConfiguration : EntityConfiguration<Flight>
{
    public override void ConfigureEntity(EntityTypeBuilder<Flight> builder)
    {
        builder.HasOne(f => f.Origin).WithMany(p => p.OriginFlights).HasForeignKey(f => f.OriginId);
        builder.HasOne(f => f.Destination).WithMany(p => p.DestinationFlights).HasForeignKey(f => f.DestinationId);
    }
}