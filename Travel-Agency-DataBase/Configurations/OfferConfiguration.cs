using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain.Offers;

namespace Travel_Agency_DataBase.Configurations;

public class OfferConfiguration : EntityConfiguration<Offer>
{
    public override void ConfigureEntity(EntityTypeBuilder<Offer> builder)
    {
        builder.HasOne(o => o.Image).WithMany().HasForeignKey(o => o.ImageId);
        builder.HasOne(o => o.Agency).WithMany().HasForeignKey(o => o.AgencyId);
        builder.HasMany(o => o.Facilities).WithMany(f => f.Offers);
    }
}