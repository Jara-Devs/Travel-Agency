using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Travel_Agency_Core.Enums;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain.Offers;

namespace Travel_Agency_DataBase.Configurations;

public class ExcursionOfferConfiguration : EntityConfiguration<ExcursionOffer>
{
    protected override bool ConfigureKey => false;

    public override void ConfigureEntity(EntityTypeBuilder<ExcursionOffer> builder)
    {
        builder.HasMany(o => o.Packages).WithMany(p => p.ExcursionOffers);
        builder.HasOne(o => o.Excursion).WithMany(e => e.Offers).HasForeignKey(o => o.ExcursionId);
    }
}