using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain.Offers;

namespace Travel_Agency_DataBase.Configurations;

public class FacilityConfiguration : EntityConfiguration<Facility>
{
    public override void ConfigureEntity(EntityTypeBuilder<Facility> builder)
    {
        builder.HasIndex(x => x.Name).IsUnique();
    }
}