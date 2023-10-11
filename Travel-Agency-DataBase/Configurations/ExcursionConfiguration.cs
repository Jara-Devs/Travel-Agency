using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_DataBase.Configurations;

public class ExcursionConfiguration : EntityConfiguration<Excursion>
{
    public override void ConfigureEntity(EntityTypeBuilder<Excursion> builder)
    {
        builder.HasMany(e => e.Places).WithMany(p => p.Excursions);
        builder.HasMany(e => e.Activities).WithMany(t => t.Excursions);
    }
}