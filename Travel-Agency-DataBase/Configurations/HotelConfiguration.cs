using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_DataBase.Configurations;

public class HotelConfiguration : EntityConfiguration<Hotel>
{
    public override void ConfigureEntity(EntityTypeBuilder<Hotel> builder)
    {
        builder.HasIndex(x => x.Name).IsUnique();
        builder.HasOne(h => h.TouristPlace).WithMany(p => p.Hotels).HasForeignKey(h => h.TouristPlaceId);
    }
}