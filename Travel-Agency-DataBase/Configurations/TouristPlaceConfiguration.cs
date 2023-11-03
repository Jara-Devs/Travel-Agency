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
        builder.HasMany(tp => tp.Flights1)
            .WithOne(f => f.Place1)
            .HasForeignKey(f => f.Place1Id);
        builder.HasMany(tp => tp.Flights2)
            .WithOne(f => f.Place2)
            .HasForeignKey(f => f.Place2Id);
    }
}