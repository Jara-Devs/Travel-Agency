using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_DataBase.Configurations;

public class OverNightExcursionConfiguration : EntityConfiguration<OverNightExcursion>
{
    protected override bool ConfigureKey => false;

    public override void ConfigureEntity(EntityTypeBuilder<OverNightExcursion> builder)
    {
        builder.HasOne(e => e.Hotel).WithMany(h => h.OverNightExcursions).HasForeignKey(e => e.HotelId);
    }
}