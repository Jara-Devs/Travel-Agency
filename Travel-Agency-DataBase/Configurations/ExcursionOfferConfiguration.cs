using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain.Offers;

namespace Travel_Agency_DataBase.Configurations;

public class ExcursionOfferConfiguration : EntityConfiguration<ExcursionOffer>
{
    protected override bool ConfigureKey => false;

    public override void ConfigureEntity(EntityTypeBuilder<ExcursionOffer> builder)
    {
        builder.HasOne(o => o.Excursion).WithMany(e => e.Offers).HasForeignKey(o => o.ExcursionId);
    }
}