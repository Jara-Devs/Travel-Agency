using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel_Agency_DataBase.Core;
using Travel_Agency_Domain.Offers;

namespace Travel_Agency_DataBase.Configurations;

public class FlightOfferConfiguration : EntityConfiguration<FlightOffer>
{
    protected override bool ConfigureKey => false;

    public override void ConfigureEntity(EntityTypeBuilder<FlightOffer> builder)
    {
        builder.HasOne(o => o.Flight).WithMany(f => f.Offers).HasForeignKey(o => o.FlightId);
    }
}