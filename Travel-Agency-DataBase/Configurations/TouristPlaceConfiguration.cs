using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_DataBase.Configurations;

public class TouristPlaceConfiguration : EntityConfiguration<TouristPlace>
{
    public override void ConfigureEntity(EntityTypeBuilder<TouristPlace> builder)
    {
        builder.HasIndex(x => x.Name).IsUnique();
        builder.HasOne(t => t.Image).WithMany().HasForeignKey(t => t.ImageId);
    }
}