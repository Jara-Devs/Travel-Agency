using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_DataBase.Configurations;

public class TouristActivityConfiguration : EntityConfiguration<TouristActivity>
{
    public override void ConfigureEntity(EntityTypeBuilder<TouristActivity> builder)
    {
        builder.HasIndex(x => x.Name).IsUnique();
    }
}