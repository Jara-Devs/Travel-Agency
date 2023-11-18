using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain.Services;

namespace Travel_Agency_DataBase.Configurations;

public class ExcursionConfiguration : EntityConfiguration<Excursion>
{
    public override void ConfigureEntity(EntityTypeBuilder<Excursion> builder)
    {
        builder.HasIndex(x => x.Name).IsUnique();
        builder.HasMany(e => e.Places).WithMany(p => p.Excursions);
        builder.HasMany(e => e.Activities).WithMany(t => t.Excursions);
        builder.HasOne(e => e.Image).WithMany().HasForeignKey(e => e.ImageId);
    }
}