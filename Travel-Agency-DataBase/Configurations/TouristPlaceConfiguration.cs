using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_DataBase.Configurations;

public class TouristPlaceConfiguration : EntityConfiguration<TouristPlace>
{
    public override void ConfigureEntity(EntityTypeBuilder<TouristPlace> builder)
    {
        builder.HasIndex(x => x.Name).IsUnique();
        builder.OwnsOne(h => h.Address);
        builder.HasMany(tp => tp.OriginFlights)
            .WithOne(f => f.Origin)
            .HasForeignKey(f => f.OriginId);
        builder.HasMany(tp => tp.DestinationFlights)
            .WithOne(f => f.Destination)
            .HasForeignKey(f => f.DestinationId);
        builder.HasOne(t => t.Image).WithMany().HasForeignKey(t => t.ImageId);
    }
}